@Html.DevExpress().GridView(Sub(settings)
                                settings.Name = "GridView"
                                settings.KeyFieldName = "Id"
                                settings.CallbackRouteValues = New With {.Controller = "Test2", .Action = "GridViewPartial"}
                                settings.SettingsEditing.BatchUpdateRouteValues = New With {.Controller = "Test2", .Action = "BatchUpdatePartial"}
                                settings.SettingsEditing.Mode = GridViewEditingMode.Batch
                                settings.CommandColumn.Visible = True
                                settings.CommandColumn.ShowDeleteButton = True
                                settings.CommandColumn.ShowNewButtonInHeader = True
                                settings.SettingsEditing.ShowModelErrorsForEditors = True
                                settings.ClientSideEvents.EndCallback = "OnEndCallback"
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
                                settings.CellEditorInitialize = Sub(s, e)
                                                                    Dim editor As ASPxEdit = e.Editor
                                                                    editor.ValidationSettings.Display = Display.Dynamic
                                                                End Sub
                                settings.CommandButtonInitialize = Sub(s, e)
    
                                                                       If e.ButtonType = ColumnCommandButtonType.Update OrElse e.ButtonType = ColumnCommandButtonType.Cancel Then
                                                                           e.Visible = False
                                                                       End If
                                                                   End Sub
                                settings.CustomJSProperties = Sub(s, e)
                                                                  If ViewData("ErrorFlag") IsNot Nothing Then
                                                                      e.Properties.Add("cpErrorFlag", CBool(ViewData("ErrorFlag")))
                                                                  End If                                                                 
                                                              End Sub
                            End Sub).Bind(Model).GetHtml()
