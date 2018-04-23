Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Web
Imports DevExpress.Web.Mvc
Imports System.ComponentModel

Public Class GridDataItem
	Private privateId As Integer
	Public Property Id() As Integer
		Get
			Return privateId
		End Get
		Set(ByVal value As Integer)
			privateId = value
		End Set
	End Property
	Private privateC1 As Integer
	<Required(ErrorMessage := "Field is required")> _
	Public Property C1() As Integer
		Get
			Return privateC1
		End Get
		Set(ByVal value As Integer)
			privateC1 = value
		End Set
	End Property
	Private privateC2 As Double
	<Range(0, 100, ErrorMessage := "Must be between 0 and 100")> _
	Public Property C2() As Double
		Get
			Return privateC2
		End Get
		Set(ByVal value As Double)
			privateC2 = value
		End Set
	End Property
	Private privateC3 As String
	<Required(ErrorMessage := "Field is required"), StringLength(10, ErrorMessage := "Must be under 10 characters")> _
	Public Property C3() As String
		Get
			Return privateC3
		End Get
		Set(ByVal value As String)
			privateC3 = value
		End Set
	End Property
	Private privateC4 As Boolean
	<Display(Name := "Current State"), Required(ErrorMessage := "State is required")> _
	Public Property C4() As Boolean
		Get
			Return privateC4
		End Get
		Set(ByVal value As Boolean)
			privateC4 = value
		End Set
	End Property
	Private privateC5 As DateTime
	<DisplayName ("Current Date"), Required(ErrorMessage := "Date is required")> _
	Public Property C5() As DateTime
		Get
			Return privateC5
		End Get
		Set(ByVal value As DateTime)
			privateC5 = value
		End Set
	End Property
End Class

Public Class BatchEditRepository
	Public Shared ReadOnly Property GridData() As List(Of GridDataItem)
		Get
			Dim key = "34FAA431-CF79-4869-9488-93F6AAE81263"
			Dim Session = HttpContext.Current.Session
			If Session(key) Is Nothing Then
				Session(key) = Enumerable.Range(0, 100).Select(Function(i) New GridDataItem With {.Id = i, .C1 = i Mod 2, .C2 = i * 0.5 Mod 3, .C3 = "C3 " & i, .C4 = i Mod 2 = 0, .C5 = New DateTime(2013 + i, 12, 16)}).ToList()
			End If
			Return CType(Session(key), List(Of GridDataItem))
		End Get
	End Property

	Public Shared Sub InsertNewItem(ByVal postedItem As GridDataItem, ByVal batchValues As MVCxGridViewBatchUpdateValues(Of GridDataItem, Integer))
		Try
			Dim newItem = New GridDataItem() With {.Id = GridData.Count}
			LoadNewValues(newItem, postedItem)
			GridData.Add(newItem)

		Catch e As Exception
			batchValues.SetErrorText(postedItem, e.Message)
		End Try
	End Sub

	Public Shared Sub UpdateItem(ByVal postedItem As GridDataItem, ByVal batchValues As MVCxGridViewBatchUpdateValues(Of GridDataItem, Integer))
		Try
			Dim editedItem = GridData.First(Function(i) i.Id = postedItem.Id)
			LoadNewValues(editedItem, postedItem)
		Catch e As Exception
			batchValues.SetErrorText(postedItem, e.Message)
		End Try
	End Sub

	Public Shared Sub DeleteItem(ByVal itemKey As Integer, ByVal batchValues As MVCxGridViewBatchUpdateValues(Of GridDataItem, Integer))
		Try
			Dim item = GridData.First(Function(i) i.Id = itemKey)
			GridData.Remove(item)
		Catch e As Exception
			batchValues.SetErrorText(itemKey, e.Message)
		End Try
	End Sub

	Protected Shared Sub LoadNewValues(ByVal newItem As GridDataItem, ByVal postedItem As GridDataItem)
		newItem.C1 = postedItem.C1
		newItem.C2 = postedItem.C2
		newItem.C3 = postedItem.C3
		newItem.C4 = postedItem.C4
		newItem.C5 = postedItem.C5
	End Sub
End Class