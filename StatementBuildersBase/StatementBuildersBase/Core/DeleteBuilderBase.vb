Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports System.Text
Imports SSP.Data.StatementBuildersBase.Core.Enums
Imports SSP.Data.StatementBuildersBase.Core.Interfaces
#End Region

Namespace Core

	Public MustInherit Class DeleteBuilderBase

		Inherits BuilderBase
		Implements IBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private _table As String
		Private ReadOnly _where As New WhereBuilderList
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New(ByVal commentChar As String)
			Me.New(commentChar, "")
		End Sub

		Public Sub New(ByVal startCommentChar As String, ByVal endCommentChar As String)
			MyBase.New(startCommentChar, endCommentChar)
			Me.BuilderLists.Add("WHERE", Me.Where)
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public Property Table() As String
			Get
				Return _table
			End Get
			Set(ByVal value As String)
				_table = value
			End Set
		End Property

		Public ReadOnly Property Where() As WhereBuilderList
			Get
				Return _where
			End Get
		End Property
#End Region

#Region " --------------->> Private Methoden der Klasse "
		Protected Overridable Function GetStatementOnly() As String

			Dim sb As New StringBuilder

			sb.AppendLine("DELETE FROM ")
			sb.AppendLine(vbTab & _table)

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
		Public MustOverride Function ExecuteNonQuery(ByVal connectionString As String) As Int64

		Public MustOverride Function ExecuteNonQuery() As Int64

		Public Overrides Function ToString() As String Implements IBuilderBase.ToString

			Return GetStatement()
		End Function

		Public Overloads Function ToString _
		(ByVal statementFormat As StatementFormats) As String _
		Implements IBuilderBase.ToString

			Return GetStatement(statementFormat)
		End Function

		'''<summary>Liefert das Statement als Plaintext.</summary>
		Public Function GetStatement() As String Implements IBuilderBase.GetStatement

			Return GetStatement(StatementFormats.StatementAndMetaData)
		End Function

		'''<summary>Liefert das Statement als Plaintext.</summary>
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