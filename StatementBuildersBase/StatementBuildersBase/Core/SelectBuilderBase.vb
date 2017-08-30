Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports System.Text
Imports SSP.Data.StatementBuildersBase.Core.Enums
Imports SSP.Data.StatementBuildersBase.Core.Interfaces
#End Region

Namespace Core

	Public MustInherit Class SelectBuilderBase

		Inherits BuilderBase

		Implements IBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private ReadOnly _select As New BuilderList(",")
		Private ReadOnly _from As New BuilderList("")
		Private ReadOnly _where As New WhereBuilderList
		Private ReadOnly _group As New BuilderList(",")
		Private ReadOnly _having As New BuilderList("")
		Private ReadOnly _order As New BuilderList(",")
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New(ByVal commentChar As String)
			Me.New(commentChar, "")
		End Sub

		Public Sub New(ByVal startCommentChar As String, ByVal endCommentChar As String)
			MyBase.New(startCommentChar, endCommentChar)
			MyBase.BuilderLists.Add("SELECT", Me.Select)
			MyBase.BuilderLists.Add("FROM", Me.From)
			MyBase.BuilderLists.Add("WHERE", Me.Where)
			MyBase.BuilderLists.Add("GROUP BY", Me.Group)
			MyBase.BuilderLists.Add("HAVING", Me.Having)
			MyBase.BuilderLists.Add("ORDER BY", Me.Order)
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public ReadOnly Property [Select]() As BuilderList
			Get
				Return _select
			End Get
		End Property

		Public ReadOnly Property From() As BuilderList
			Get
				Return _from
			End Get
		End Property

		Public ReadOnly Property Where() As WhereBuilderList
			Get
				Return _where
			End Get
		End Property

		Public ReadOnly Property Group() As BuilderList
			Get
				Return _group
			End Get
		End Property

		Public ReadOnly Property Having() As BuilderList
			Get
				Return _having
			End Get
		End Property

		Public ReadOnly Property Order() As BuilderList
			Get
				Return _order
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
		End Function
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		Public MustOverride Function ExecuteReader() As IDataReader
		Public MustOverride Function ExecuteReader(ByVal connectionString As String) As IDataReader
		Public MustOverride Function ExecuteScalar() As Object
		Public MustOverride Function ExecuteScalar(ByVal connectionString As String) As Object
		Public MustOverride Function ExecuteStringScalar() As String
		Public MustOverride Function ExecuteStringScalar(ByVal connectionString As String) As String

		Public Overrides Function ToString() As String Implements IBuilderBase.ToString
			Return GetStatement(StatementFormats.StatementAndMetaData)
		End Function

		Public Overloads Function ToString _
		(ByVal statementFormat As StatementFormats) As String _
		Implements IBuilderBase.ToString

			Return GetStatement(statementFormat)
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
					sb = New StringBuilder(GetStatementOnly)
					'sb.Append(GetStatementOnly)
				Case Else
					sb = New StringBuilder()
			End Select

			Return sb.ToString
		End Function

		Public Function GetStatement() As String Implements IBuilderBase.GetStatement

			Return GetStatement(StatementFormats.StatementAndMetaData)
		End Function
#End Region

	End Class

End Namespace