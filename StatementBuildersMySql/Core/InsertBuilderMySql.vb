Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports Devart.Data.MySql
Imports System.Text
Imports SSP.Data.StatementBuildersBase.Core
#End Region

Namespace Core

	Public Class InsertBuilderMySql

		Inherits InsertBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New()
			MyBase.New("#")
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
		Protected Overrides Function GetStatementOnly() As String
			Dim sb As New StringBuilder

			sb.AppendLine("INSERT INTO " & Table)
			sb.AppendLine(vbTab & "(")
			For Each s As String In _fieldsAndValues.Keys
				sb.AppendLine(vbTab & vbTab & s & ",")
			Next s
			If _fieldsAndValues.Count > 0 Then sb.Remove(sb.Length - 3, 1)

			sb.AppendLine(vbTab & ")")
			sb.AppendLine("VALUES")
			sb.AppendLine(vbTab & "(")
			For Each s As String In _fieldsAndValues.Values
				sb.AppendLine(vbTab & vbTab & s & ",")
			Next s
			If _fieldsAndValues.Count > 0 Then sb.Remove(sb.Length - 3, 1)

			sb.AppendLine(vbTab & ")")

			Return sb.ToString
		End Function

#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		'''<summary>Führt das Insert-Statement unter Verwendung der Default-Connection aus.</summary>
		Public Overrides Function ExecuteNonQuery() As Int64
			Return ExecuteNonQuery(DbResultMySql.DefaultConnectionString)
		End Function

		'''<summary>Führt das Insert-Statement aus.</summary>
		Public Overrides Function ExecuteNonQuery(connectionString As String) As Int64
			If Me.Parameters.Count = 0 Then
				Return DbResultMySql.Instance.ExecuteNonQuery(connectionString, Me.ToString)
			Else
				Return DbResultMySql.Instance.ExecuteNonQuery(connectionString, Me.ToString _
				, Me.Parameters.GetIDbParameters(Of MySqlParameter))
			End If
		End Function
#End Region

	End Class

End Namespace