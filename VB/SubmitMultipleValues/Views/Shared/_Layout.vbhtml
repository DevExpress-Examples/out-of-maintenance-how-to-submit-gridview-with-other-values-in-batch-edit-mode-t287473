<!DOCTYPE html>

<html>
<head>
    <title>@ViewBag.Title</title>

    @Html.DevExpress().GetStyleSheets(
    New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
    New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
    New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView}
)
    @Html.DevExpress().GetScripts(
    New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
    New Script With {.ExtensionSuite = ExtensionSuite.Editors},
    New Script With {.ExtensionSuite = ExtensionSuite.GridView}
)
</head>

<body>
    @RenderBody()
</body>
</html>
