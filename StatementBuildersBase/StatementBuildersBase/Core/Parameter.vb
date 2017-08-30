Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core

	Public Class Parameter

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private _name As String
		Private _value As Object
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New()
		End Sub

		Public Sub New _
		(ByVal name As String _
		, ByVal value As Object)

			Initialize(name, value)
		End Sub

		Private Sub Initialize _
		(ByVal name As String _
		, ByVal value As Object)

			_name = name
			_value = value
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				_name = value
			End Set
		End Property

		Public Property Value() As Object
			Get
				Return _value
			End Get
			Set(ByVal value As Object)
				_value = value
			End Set
		End Property
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
#End Region

	End Class
End Namespace