@Code
    ViewBag.Title = "How to submit GridView with other values in one Action"
End Code
@ModelType SubmitMultipleValues.Models.MainModel

<script>   
    function OnClick(s, e) {   
        if ($("#myForm").valid()) {
            if (GridView.batchEditApi.HasChanges())
                GridView.UpdateEdit();
            else
                GridView.PerformCallback();
        }
    }
    function OnEndCallback(s, e) {
        if (s.cpSuccessFlag)
        {
            alert("All changes were saved");
            window.location = "@Url.Action("Success","Test1")";
        }
    }
    function OnBeginCallback(s, e) {
        var object = { ID: ID.GetValue(), Name: Name.GetValue(), Description: Description.GetValue() };
        e.customArgs["mainModel"] = JSON.stringify(object);
    } 
</script>


<h2>How to submit GridView with other values in one Action</h2>

@Using(Html.BeginForm("Index", "Test1", FormMethod.Post, New With{ .id = "myForm" }))
   
    @Html.DevExpress().FormLayout(Sub(settings)

                                       settings.Name = "FormLayout"
                                       Dim GroupAction As Action (Of DevExpress.Web.Mvc.MVCxFormLayoutGroup(Of SubmitMultipleValues.Models.MainModel))  =  Sub (groupSettings)    
                                                                                       groupSettings.Caption = "Main information"
                                                                                       groupSettings.ShowCaption = DefaultBoolean.True
                                                                                       groupSettings.GroupBoxDecoration = GroupBoxDecoration.Default
                                        End Sub
                                       Dim groupItem As MVCxFormLayoutGroup(Of SubmitMultipleValues.Models.MainModel) = settings.Items.AddGroupItem(GroupAction)

                                       groupItem.SettingsItems.ShowCaption = DefaultBoolean.True
                                       groupItem.SettingsItemCaptions.Location = LayoutItemCaptionLocation.Left

                                       groupItem.SettingsItemHelpTexts.Position = HelpTextPosition.Auto

       groupItem.Items.Add(Function(m) m.ID, Sub(item)    
                                              item.HelpText = "Help text for ID"
                                              Dim s As SpinEditSettings = TryCast(item.NestedExtensionSettings, SpinEditSettings)
                                              s.ShowModelErrors = True
    End Sub)
    groupItem.Items.Add(Function(m)  m.Name, Sub(item)
                                                 Dim s As TextBoxSettings = TryCast(item.NestedExtensionSettings, TextBoxSettings)
                                                 s.ShowModelErrors = True
                                                 item.HelpText = "Help text for Name"
    End Sub)
    groupItem.Items.Add(Function(m) m.Description, Sub(item)
    
                                                       item.NestedExtensionType = FormLayoutNestedExtensionItemType.Memo
         Dim s As MemoSettings = TryCast(item.NestedExtensionSettings, MemoSettings)
                                                       s.ShowModelErrors = True
                                                       item.HelpText = "Help text for Description"
    End Sub)
    settings.Items.AddGroupItem(Sub(groupSettings)
    
        groupSettings.Caption = "Details"
        groupSettings.ShowCaption = DefaultBoolean.True
        groupSettings.Items.Add(Sub(itemSettings)
        
            itemSettings.ShowCaption = DefaultBoolean.False
            itemSettings.SetNestedContent(Sub() 
            
                Html.RenderAction("GridViewPartial")
            End Sub)
        End Sub)
    End Sub)
End Sub).GetHtml()
    @Html.DevExpress().Button(Sub(settings)

    settings.Name = "btnSubmit"
    settings.Text = "Submit Changes"
    settings.UseSubmitBehavior = false
    settings.ClientSideEvents.Click = "OnClick"
End Sub).GetHtml()
    @Html.DevExpress().Button(Sub(settings)

    settings.Name = "btnCancel"
    settings.Text = "Cancel Changes"
    settings.ClientSideEvents.Click = "function(s, e) { ASPxClientEdit.ClearEditorsInContainer(null);  GridView.CancelEdit(); }"
End Sub).GetHtml()
 
End Using



