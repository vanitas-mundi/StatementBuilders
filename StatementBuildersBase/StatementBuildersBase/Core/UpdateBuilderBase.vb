Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports System.Text
Imports SSP.Data.StatementBuildersBase.Core.Enums
Imports SSP.Data.StatementBuildersBase.Core.Interfaces
#End Region

Namespace Core

	Public MustInherit Class UpdateBuilderBase

		Inherits BuilderBase
		Implements IBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private ReadOnly _updateTables As New BuilderList(",")
		Private ReadOnly _set As New BuilderList(",")
		Private ReadOnly _where As New WhereBuilderList
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New(ByVal commentChar As String)
			Me.New(commentChar, "")
		End Sub

		Public Sub New(ByVal startCommentChar As String, ByVal endCommentChar As String)
			MyBase.New(startCommentChar, endCommentChar)
			Me.BuilderLists.Add("UPDATE", Me.UpdateTables)
			Me.BuilderLists.Add("SET", Me.Set)
			Me.BuilderLists.Add("WHERE", Me.Where)
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public ReadOnly Property UpdateTables() As BuilderList
			Get
				Return _updateTables
			End Get
		End Property

		Public ReadOnly Property [Set]() As BuilderList
			Get
				Return _set
			End Get
		End Property

		Public ReadOnly Property Where() As WhereBuilderList
			Get
				Return _where
			End Get
		End Property
#End Region

#Region " --------------->> Private Methoden der Klasse "
		Protected Overridable Function GetStatementOnly() As String

			Dim sb = New StringBuilder

			For Each builderListName In _builderLists.Keys
				Dim bl = _builderLists.Item(builderListName)
				Dim ar = bl.ToList.Select(Function(s, index) vbTab & s & If(index + 1 < bl.Count, bl.Delimiter, "").ToString).ToArray()
				If bl.Count > 0 Then
					sb.AppendLine(builderListName)
					sb.AppendLine(String.Join(vbCrLf, ar))
				End If
			Next builderListName

			Return sb.ToString

			'Dim sb As New StringBuilder

			'sb.AppendLine("UPDATE")

			'For Each s As String In _updateTables
			'	sb.AppendLine(vbTab & s & ",")
			'Next s
			'If _updateTables.Count > 0 Then sb.Remove(sb.Length - 3, 1)

			'sb.AppendLine("SET")
			'For Each s As String In _set
			'	sb.AppendLine(vbTab & s & ",")
			'Next s
			'If _set.Count > 0 Then sb.Remove(sb.Length - 3, 1)

			'If _where.Count > 0 Then
			'	sb.AppendLine("WHERE")
			'	For Each s As String In _where
			'		sb.AppendLine(vbTab & s)
			'	Next s
			'End If

			'Return sb.ToString
		End Function
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		'''<summary>Führt das Update-Statement unter Verwendung des Default-ConnectionString aus.</summary>
		Public MustOverride Function ExecuteNonQuery() As Int64

		'''<summary>Führt das Update-Statement aus.</summary>
		Public MustOverride Function ExecuteNonQuery(ByVal connectionString As String) As Int64

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Wert value in das Update-Statement ein.
		'''Value muss bei der Übergabe datenbakkomform gequotet und maskiert sein.
		'''</summary>
		Public Sub AddFieldAndValue(ByVal columnName As String, ByVal value As String)

			Me.Set.Add(String.Format("{0} = {1}", columnName, value))
		End Sub

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Wert value in das Update-Statement ein.
		'''Value wird über das quotingRules-Objekt datenbakkomform gequotet und maskiert.
		'''</summary>
		Public Sub AddFieldAndValue(Of TValue As IConvertible) _
		(ByVal columnName As String, ByVal value As TValue, ByVal quotingRules As IValueQuotingRules)

			Me.Set.Add(String.Format("{0} = {1}", columnName, quotingRules.GetQuotedValue(Of TValue)(value)))
		End Sub

		Public Overrides Function ToString() As String _
		Implements IBuilderBase.ToString

			Return GetStatement(StatementFormats.StatementAndMetaData)
		End Function

		Public Overloads Function ToString _
		(ByVal statementFormat As StatementFormats) As String _
		Implements IBuilderBase.ToString

			Return GetStatement(statementFormat)
		End Function

		Public Function GetStatement() As String _
		Implements IBuilderBase.GetStatement

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