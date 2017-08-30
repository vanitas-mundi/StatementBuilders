Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core.Interfaces
Imports SSP.Data.StatementBuildersBase.Core.Enums
Imports System.Text
#End Region

Namespace Core

	Public MustInherit Class InsertBuilderBase

		Inherits BuilderBase
		Implements IBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Protected _table As String
		Protected ReadOnly _fieldsAndValues As New BuilderDictionary
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New(ByVal commentChar As String)
			Me.New(commentChar, "")
		End Sub

		Public Sub New(ByVal startCommentChar As String, ByVal endCommentChar As String)
			MyBase.New(startCommentChar, endCommentChar)
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		'''<summary>Die zugrunde liegende Datenbanktabelle (z.B. datapool.t_personen)</summary>
		Public Property Table() As String
			Get
				Return _table
			End Get
			Set(ByVal value As String)
				_table = value
			End Set
		End Property

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Wert value in das Insert-Statement ein.
		'''Value muss bei der Übergabe datenbakkomform gequotet und maskiert sein.
		'''Alternativ kann AddFieldAndValue genutzt werden.
		'''</summary>
		Public ReadOnly Property FieldsAndValues() As BuilderDictionary
			Get
				Return _fieldsAndValues
			End Get
		End Property
#End Region

#Region " --------------->> Private Methoden der Klasse "
		Protected MustOverride Function GetStatementOnly() As String
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		Public MustOverride Function ExecuteNonQuery() As Int64

		Public MustOverride Function ExecuteNonQuery(ByVal connectionString As String) As Int64

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Wert value in das Insert-Statement ein.
		'''Value muss bei der Übergabe datenbakkomform gequotet und maskiert sein.
		'''</summary>
		Public Sub AddFieldAndValue(ByVal columnName As String, ByVal value As String)

			_fieldsAndValues.Add(columnName, value)
		End Sub

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Wert value in das Insert-Statement ein.
		'''Value wird über das quotingRules-Objekt datenbakkomform gequotet und maskiert.
		'''</summary>
		Public Sub AddFieldAndValue(Of TValue As IConvertible) _
		(ByVal columnName As String, ByVal value As TValue, ByVal quotingRules As IValueQuotingRules)

			_fieldsAndValues.Add(columnName, quotingRules.GetQuotedValue(Of TValue)(value))
		End Sub

		Public Overrides Function ToString() As String _
		Implements IBuilderBase.ToString

			Return GetStatement(StatementFormats.StatementAndMetaData)
		End Function

		Public Overloads Function ToString _
		(ByVal statementFormat As StatementFormats) As String _
		Implements IBuilderBase.ToString

			Return (GetStatement(statementFormat))
		End Function

		Public Function GetStatement() As String Implements IBuilderBase.GetStatement

			Return GetStatement(StatementFormats.StatementAndMetaData)
		End Function

		Public Function GetStatement _
		(ByVal statementFormat As StatementFormats) As String _
		Implements IBuilderBase.GetStatement

			Dim sb As StringBuilder
			Select Case statementFormat
				Case StatementFormats.StatementAndMetaData
					sb = New StringBuilder(MyBase.ToString)
					sb.Append(GetStatementOnly)
				Case StatementFormats.MetaDataOnly
					sb = New StringBuilder(MyBase.ToString)
				Case StatementFormats.StatementOnly
					sb = New StringBuilder()
					sb.Append(GetStatementOnly)
				Case Else
					sb = New StringBuilder()
			End Select

			Return sb.ToString
		End Function
#End Region

	End Class

End Namespace