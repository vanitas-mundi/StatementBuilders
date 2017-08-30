Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports Devart.Data.MySql
Imports SSP.Data.StatementBuildersBase.Core
Imports System.Text
#End Region

Namespace Core

	Public Class SelectBuilderMySql

		Inherits SelectBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private _limit As Int32 = 0
		Private _limitPosition As Int32 = 0
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New()
			MyBase.New("#")
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public Property Limit() As Int32
			Get
				Return _limit
			End Get
			Set(ByVal value As Int32)
				_limit = value
			End Set
		End Property

		Public Property LimitPosition() As Int32
			Get
				Return _limitPosition
			End Get
			Set(ByVal value As Int32)
				_limitPosition = value
			End Set
		End Property
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		'''<summary>Führt das Select-Statement aus unter Verwendung des Default-ConnectionStrings.</summary>
		Public Overrides Function ExecuteReader() As IDataReader
			Return ExecuteReader(DbResultMySql.DefaultConnectionString)
		End Function

		'''<summary>Führt das Select-Statement aus.</summary>
		Public Overrides Function ExecuteReader(ByVal connectionString As String) As IDataReader

			If Me.Parameters.Count = 0 Then
				Return DbResultMySql.Instance.ExecuteReader(connectionString, Me.ToString)
			Else
				Return DbResultMySql.Instance.ExecuteReader(connectionString, Me.ToString _
					, Me.Parameters.GetIDbParameters(Of MySqlParameter))
			End If
		End Function

		'''<summary>Führt das Select-Statement aus unter Verwendung des Default-ConnectionStrings.</summary>
		Public Overrides Function ExecuteScalar() As Object
			Return ExecuteScalar(DbResultMySql.DefaultConnectionString)
		End Function

		'''<summary>Führt das Select-Statement aus.</summary>
		Public Overrides Function ExecuteScalar(ByVal connectionString As String) As Object

			Select Case Me.Parameters.Count
				Case 0
					Return DbResultMySql.Instance.ExecuteScalar(connectionString, Me.ToString)
				Case Else
					Return DbResultMySql.Instance.ExecuteScalar(connectionString, Me.ToString _
					, Me.Parameters.GetIDbParameters(Of MySqlParameter))
			End Select
		End Function

		'''<summary>Führt das Select-Statement aus unter Verwendung des Default-ConnectionStrings aus.</summary>
		Public Overrides Function ExecuteStringScalar() As String
			Return ExecuteStringScalar(DbResultMySql.DefaultConnectionString)
		End Function

		'''<summary>Führt das Select-Statement aus.</summary>
		Public Overrides Function ExecuteStringScalar(ByVal connectionString As String) As String

			Select Case Me.Parameters.Count
				Case 0
					Return DbResultMySql.Instance.ExecuteStringScalar(connectionString, Me.ToString)
				Case Else
					Return DbResultMySql.Instance.ExecuteScalar(connectionString, Me.ToString _
					, Me.Parameters.GetIDbParameters(Of MySqlParameter)).ToString
			End Select
		End Function

		'''<summary>Liefert das Statement als Plaintext.</summary>
		Protected Overrides Function GetStatementOnly() As String

			Dim statement = MyBase.GetStatementOnly()

			If _limit > 0 Then
				Dim limitLine = New StringBuilder
				limitLine.AppendLine("LIMIT")
				limitLine.Append(vbTab)

				If _limitPosition > 0 Then limitLine.Append(_limitPosition & ", ")
				limitLine.Append(_limit)
				statement &= limitLine.ToString
			End If

			Return statement
		End Function
#End Region

	End Class

End Namespace