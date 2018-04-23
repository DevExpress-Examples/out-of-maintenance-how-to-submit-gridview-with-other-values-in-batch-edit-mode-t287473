Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports SubmitMultipleValues.Models
Imports System.Web.Script.Serialization

Namespace SubmitMultipleValues.Controllers
	Public Class Test1Controller
		Inherits Controller

		Public Function Index() As ActionResult
			Return View(New MainModel())
		End Function

		Public Function GridViewPartial() As ActionResult

			Return PartialView(BatchEditRepository.GridData)
		End Function
		Public Function GridViewCustomActionUpdate(ByVal mainModel As String) As ActionResult
			ViewData("SuccessFlag") = UpdateAllValues(Nothing, mainModel)
			Return PartialView("GridViewPartial", BatchEditRepository.GridData)
		End Function
		' save all changes to a data base in this action 
		Public Function UpdateAllValues(ByVal batchValues As MVCxGridViewBatchUpdateValues(Of GridDataItem, Integer), ByVal mainModel As String) As Boolean
			If batchValues IsNot Nothing Then
				For Each item In batchValues.Insert
					If batchValues.IsValid(item) Then
						BatchEditRepository.InsertNewItem(item, batchValues)
					Else
						batchValues.SetErrorText(item, "Correct validation errors")
					End If
				Next item
				For Each item In batchValues.Update
					If batchValues.IsValid(item) Then
						BatchEditRepository.UpdateItem(item, batchValues)
					Else
						batchValues.SetErrorText(item, "Correct validation errors")
					End If
				Next item
				For Each itemKey In batchValues.DeleteKeys
					BatchEditRepository.DeleteItem(itemKey, batchValues)
				Next itemKey
			End If
			Dim result As Boolean = False
			If (Not String.IsNullOrEmpty(mainModel)) Then
				Dim jss As New JavaScriptSerializer()
				Dim m As MainModel = jss.Deserialize(Of MainModel)(mainModel)
				'custom actions
				result = True
			End If
			Return result AndAlso (batchValues Is Nothing OrElse batchValues.EditorErrors.Count = 0)
		End Function
		Public Function BatchUpdatePartial(ByVal batchValues As MVCxGridViewBatchUpdateValues(Of GridDataItem, Integer), ByVal mainModel As String) As ActionResult
		   ViewData("SuccessFlag") = UpdateAllValues(batchValues, mainModel)
			Return PartialView("GridViewPartial", BatchEditRepository.GridData)
		End Function
		Public Function Success() As ActionResult
			Return View("Success")
		End Function

	End Class
End Namespace
