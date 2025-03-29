window.loadRevealView = async function (viewId, dashboardName) {
    let rvDashboard;

    if (dashboardName) {
        rvDashboard = await $.ig.RVDashboard.loadDashboard(dashboardName);
    }

    const revealView = createRevealView(viewId, dashboardName);
    revealView.interactiveFilteringEnabled = true;

    console.log("dashboardName: " + dashboardName);

    if (!rvDashboard) {
        revealView.startInEditMode = true;

        revealView.onDataSourcesRequested = (callback) => {
            const restDataSource = new $.ig.RVRESTDataSource();
            restDataSource.id = "RestDataSource";
            restDataSource.url = "https://excel2json.io/api/share/8bf0acfa-7fd8-468e-0478-08daa4a8d995";
            restDataSource.title = "Auto Users Data - Global";
            restDataSource.subtitle = "from Excel2Json";
            restDataSource.useAnonymousAuthentication = true;
            callback(new $.ig.RevealDataSources([restDataSource], [], false));
        };
    }

    revealView.onDashboardSelectorRequested = (args) => {
        openDialog(args.callback);
    };

    revealView.onLinkedDashboardProviderAsync = (dashboardId, title) => {
        return $.ig.RVDashboard.loadDashboard(dashboardId);
    };

    revealView.dashboard = rvDashboard;
}

window.createRevealView = function (viewId, singleVisualizationMode) {
    $.ig.RevealSdkSettings.theme = createRevealTheme(viewId, singleVisualizationMode);

    const revealView = new $.ig.RevealView("#" + viewId);
    //revealView.serverSideSave = false;
    if (singleVisualizationMode) {
        revealView.singleVisualizationMode = true;
        revealView.showHeader = false;
        revealView.showMenu = false;
    }

    revealView.onSave = (rv, args) => {
        if (args.saveAs) {
            console.log("i am saving as");
            DotNet.invokeMethodAsync('DashboardViewer', 'PromptForDashboardName')
                .then(newName => {
                    console.log("newName " + newName);
                    if (newName) {
                        isDuplicateName(newName).then(isDuplicate => {
                            if (isDuplicate === "true") {
                                console.log("I am saving dot net");
                                DotNet.invokeMethodAsync('DashboardViewer', 'ConfirmOverride', newName)
                                    .then(overrideConfirmed => {
                                        if (!overrideConfirmed) {
                                            return;
                                        }
                                        args.dashboardId = args.name = newName;
                                        args.saveFinished();
                                        console.log("SavedAs Finished " + newName);
                                        setTimeout(() => {
                                            DotNet.invokeMethodAsync('DashboardViewer', 'ReloadDashboardList');
                                        }, 250);
                                    });
                            } else {
                                args.dashboardId = args.name = newName;
                                args.saveFinished();
                                console.log("SavedAs Finished " + newName);
                                setTimeout(() => {
                                    DotNet.invokeMethodAsync('DashboardViewer', 'ReloadDashboardList');
                                }, 250);
                            }
                        });
                    }
                    else {
                        console.log("No name");
                    }
                });
        } else {
            args.saveFinished();
            console.log("Saved Finished " + args);
            setTimeout(() => {
                DotNet.invokeMethodAsync('DashboardViewer', 'ReloadDashboardList');
            }, 250);
        }
    }
    return revealView;
  }

window.createRevealTheme = function (viewId, singleVisualizationMode) {
    var theme = $.ig.RevealSdkSettings.theme.clone();
    theme.chartColors = ["#09B1A9", "#003B4A", "#93C569", "#FEB51E", "#FF780D", "#CA365B"];
    theme.regularFont = "Poppins";
    theme.mediumFont = "Poppins";
    theme.boldFont = "Poppins";
    theme.useRoundedCorners = true;
    theme.accentColor = "#09B1A9";

    if (singleVisualizationMode) {
        theme.dashboardBackgroundColor = "white";
    }
    if (viewId === "revealViewNew") {
        theme.dashboardBackgroundColor = "white";
    }
    else {
        theme.dashboardBackgroundColor = "#F5F5F5";
    }
    return theme;
}

window.createDashboardThumbnail = function (id, info) {
    console.log("info " + info);
    $.ig.RevealSdkSettings.theme = createRevealTheme(id, "");
    let thumbnailView = new $.ig.RevealDashboardThumbnailView("#" + id);
    thumbnailView.dashboardInfo = info;
}

function downloadFile(url, filename) {
    var link = document.createElement('a');
    link.href = url;
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

window.selectedDashboardCallback = async function (selectedDashboard) {
    console.log("selectedDashboardCallback called with: " + selectedDashboard);
    if (window.dialogCallback) {
        console.log("window.dialogCallback is defined, calling it now.");
        await window.dialogCallback(selectedDashboard);
    } else {
        console.error("window.dialogCallback is not defined.");
    }
}

function openDialog(callback) {
    window.dialogCallback = callback;
    DotNet.invokeMethodAsync('DashboardViewer', 'ToggleDialog')
        .then(() => {
            console.log('OpenDialog - Dialog toggled from JavaScript');
        })
        .catch(error => {
            console.error('OpenDialog - Error toggling dialog from JavaScript', error);
        });
}

async function isDuplicateName(dashboardName) {
    const response = await fetch(`/dashboards/${encodeURIComponent(dashboardName)}/isduplicate`);
    if (!response.ok) {
        throw new Error('Network response was not ok');
    }
    return await response.text();
}

window.getRVDashboardAsJson = async function (dashboardName) {
    const dashboard = await $.ig.RVDashboard.loadDashboard(dashboardName);
    const json = dashboard._dashboardModel.__dashboardModel.toJson();
    return JSON.stringify(json);
}

window.loadRevealViewWithDom = async function (viewId, json) {
    const document = dom.RdashDocument.loadFromJson(json);
    const dashboard = await document.toRVDashboard();
    var revealView = new $.ig.RevealView("#" + viewId);
    revealView.interactiveFilteringEnabled = true;
    revealView.startInEditMode = true;
    revealView.dashboard = dashboard;
    revealView.onSave = (sender, e) => {
        window.builderInstance.invokeMethodAsync('UpdateDashboardTitle', e.name);
        e.saveFinished();
    }
}

 $.ig.RevealSdkSettings.enableActionsOnHoverTooltip = true;

window.loadRevealViewSingleViz = function (viewId, dashboardName, vizId) {
    $.ig.RVDashboard.loadDashboard(dashboardName).then(dashboard => {
        var revealView = new $.ig.RevealView("#" + viewId);
        revealView.interactiveFilteringEnabled = true;
        revealView.singleVisualizationMode = true;
        revealView.showHeader = false;
        revealView.showMenu = false;
        revealView.dashboard = dashboard;
        revealView.maximizedVisualization = dashboard.visualizations.getById(vizId);
    });
}


window.registerAddDashboardInstance = function (dotnetRef) {
    window.revealAddView = new $.ig.RevealView(document.getElementById("revealView"));
    revealAddView.hoverTooltipsEnabled = true;
    revealAddView.interactiveFilteringEnabled = true;
    revealAddView.startInEditMode = true;

    revealAddView.onSave = async function () {
        await dotnetRef.invokeMethodAsync("SaveDashboard");
    };
};

window.setDashboardFilter = async function (viewId, filterTitle, filterValue) {
    let rvDashboard = await $.ig.RVDashboard.loadDashboard("Employee Sales Analysis");

    //const revealView = createRevealView(viewId, "Employee Sales Analysis");
    const revealView = new $.ig.RevealView("#" + viewId);
    revealView.dashboard = rvDashboard;

    const territoryFilter = revealView.dashboard.filters.getByTitle(filterTitle);
    if (territoryFilter) {
        territoryFilter.selectedValues = [filterValue];
    } else {
        console.warn("Filter not found:", filterTitle);
    }
}