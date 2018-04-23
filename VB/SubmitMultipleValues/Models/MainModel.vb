Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Web

Namespace SubmitMultipleValues.Models
	Public Class MainModel
		Private privateID? As Integer
		<Required> _
		Public Property ID() As Integer?
			Get
				Return privateID
			End Get
			Set(ByVal value? As Integer)
				privateID = value
			End Set
		End Property
		Private privateName As String
		<Required> _
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateDescription As String
		<Required> _
		Public Property Description() As String
			Get
				Return privateDescription
			End Get
			Set(ByVal value As String)
				privateDescription = value
			End Set
		End Property
	End Class
End Namespace