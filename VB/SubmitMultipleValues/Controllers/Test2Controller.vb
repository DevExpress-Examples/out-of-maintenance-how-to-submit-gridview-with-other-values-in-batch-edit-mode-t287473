Imports Microsoft.VisualBasic
Imports DevExpress.Web.Mvc
Imports SubmitMultipleValues.Models
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc

Namespace SubmitMultipleValues.Controllers
	Public Class Test2Controller
		Inherits Controller
		Public Function Index() As ActionResult
			Return View(New MainModel())
		End Function
		Public Function GridViewPartial() As ActionResult
			Return PartialView(BatchEditRepository.GridData)
		End Function
		Public Function BatchUpdatePartial(ByVal batchValues As MVCxGridViewBatchUpdateValues(Of GridDataItem, Integer)) As ActionResult
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
			ViewData("ErrorFlag") = batchValues.EditorErrors.Count > 0
			Return PartialView("GridViewPartial", BatchEditRepository.GridData)
		End Function
		<HttpPost> _
		Public Function PostModel(ByVal m As MainModel) As ActionResult
			If ModelState.IsValid Then
				'get Batch Edit data from a temp repository and save it to a data base with a main model 
				Return View("Success")
			Else
				Return View("Index")
			End If
		End Function
	End Class
End Namespace
