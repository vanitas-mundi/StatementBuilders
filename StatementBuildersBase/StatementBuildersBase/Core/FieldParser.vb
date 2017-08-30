Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core.Enums
#End Region

Namespace Core

	Public Class FieldParser

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private _fieldParserType As FieldParserTypes
		Private _alias As String = ""
		Private _database As String = ""
		Private _table As String = ""
		Private _name As String = ""
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New(ByVal fullField As String)

			Initialize(fullField)
		End Sub

		Private Sub Initialize(ByVal fullField As String)

			Dim temp = fullField.Split("."c)
			Select Case temp.Count
				Case 1
					_fieldParserType = FieldParserTypes.FieldOnly
          _name = temp.First
        Case 2

          _fieldParserType = FieldParserTypes.FieldAndAlias
          _alias = temp.First
          _name = temp(1)
				Case 3
					_fieldParserType = FieldParserTypes.FieldTableAndDatabase
          _database = temp.First
          _table = temp(1)
					_name = temp(2)
			End Select
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
		Public ReadOnly Property [Alias]() As String
			Get
				Return _alias
			End Get
		End Property

		Public ReadOnly Property Database() As String
			Get
				Return _database
			End Get
		End Property

		Public ReadOnly Property Table() As String
			Get
				Return _table
			End Get
		End Property

		Public ReadOnly Property Name() As String
			Get
				Return _name
			End Get
		End Property

		Public ReadOnly Property FieldParserType() _
		 As FieldParserTypes
			Get
				Return _fieldParserType
			End Get
		End Property
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		Public Overrides Function ToString() As String
			Return _name
		End Function
#End Region

	End Class
End Namespace