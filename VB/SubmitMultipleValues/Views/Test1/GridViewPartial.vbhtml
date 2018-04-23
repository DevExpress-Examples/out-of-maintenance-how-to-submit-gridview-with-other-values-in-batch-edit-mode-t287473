@Html.DevExpress().GridView(Sub(settings)
    settings.Name = "GridView"
    settings.KeyFieldName = "Id"
    
    settings.CallbackRouteValues = new With { .Controller = "Test1", .Action = "GridViewPartial" }
    settings.CustomActionRouteValues = new With{ .Controller = "Test1", .Action = "GridViewCustomActionUpdate" }
    settings.SettingsEditing.BatchUpdateRouteValues = new With{ .Controller = "Test1", .Action = "BatchUpdatePartial" }
    settings.SettingsEditing.Mode = GridViewEditingMode.Batch
   
    settings.ClientSideEvents.BeginCallback = "OnBeginCallback"
    settings.ClientSideEvents.EndCallback = "OnEndCallback"
    settings.CommandColumn.Visible = true   
    settings.CommandColumn.ShowDeleteButton = true
    settings.CommandColumn.ShowNewButtonInHeader = true
    settings.SettingsEditing.ShowModelErrorsForEditors = true
    settings.Columns.Add("C1").Width = 50
    settings.Columns.Add(Sub(column)
        column.FieldName = "C2"
        column.ColumnType = MVCxGridViewColumnType.SpinEdit
        column.Width = 70
    End Sub)    
    settings.Columns.Add("C3").Width = 70
    settings.Columns.Add(Sub(column)
        column.FieldName = "C4"
        column.ColumnType = MVCxGridViewColumnType.CheckBox
        column.Width = 30
    End Sub)
    settings.Columns.Add(Sub(column)
        column.FieldName = "C5"
        column.ColumnType = MVCxGridViewColumnType.DateEdit
        column.PropertiesEdit.DisplayFormatString = "MM/dd/yyyy"
         CType(column.PropertiesEdit, DateEditProperties).EditFormat = EditFormat.Custom
		CType(column.PropertiesEdit, DateEditProperties).EditFormatString = "MM/dd/yyyy"  
    End Sub)
   settings.CellEditorInitialize = Sub (s, e) 
        Dim editor as ASPxEdit = e.Editor
        editor.ValidationSettings.Display = Display.Dynamic
    End Sub
    settings.CustomJSProperties = Sub(s, e)
    
         If ViewData("SuccessFlag") IsNot Nothing Then
			e.Properties("cpSuccessFlag") = ViewData("SuccessFlag")
  End If

    End Sub
 settings.CommandButtonInitialize = Sub (s, e) 
       If e.ButtonType = ColumnCommandButtonType.Update OrElse e.ButtonType = ColumnCommandButtonType.Cancel Then
			e.Visible = False
       End If
           End Sub
End Sub).Bind(Model).GetHtml()
