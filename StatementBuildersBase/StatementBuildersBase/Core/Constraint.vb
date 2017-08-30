Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core

	Public Class Constraint

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private _logicalOperator As String = ""
		Private _leftFullField As String
		Private _rightFullField As String
		Private _leftField As FieldParser
		Private _rightField As FieldParser
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New _
		(ByVal leftFullField As String _
		, ByVal rightFullField As String _
		, ByVal logicalOperator As String)

			Initialize(leftFullField, rightFullField, logicalOperator)
		End Sub

		Private Sub Initialize _
		(ByVal leftFullField As String _
		, ByVal rightFullField As String _
		, ByVal logicalOperator As String)

			_leftFullField = leftFullField
			_rightFullField = rightFullField
			_logicalOperator = logicalOperator

			_leftField = New FieldParser(_leftFullField)
			_rightField = New FieldParser(_rightFullField)
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public ReadOnly Property LogicalOperator() As String
			Get
				Return _logicalOperator
			End Get
		End Property

		Public ReadOnly Property LeftField() As FieldParser
			Get
				Return _leftField
			End Get
		End Property

		Public ReadOnly Property RightField() As FieldParser
			Get
				Return _rightField
			End Get
		End Property

		Public ReadOnly Property LeftFullField() As String
			Get
				Return _leftFullField
			End Get
		End Property

		Public ReadOnly Property RightFullField() As String
			Get
				Return _rightFullField
			End Get
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