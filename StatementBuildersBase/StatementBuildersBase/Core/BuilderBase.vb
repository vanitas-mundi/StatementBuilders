Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports System.Text
Imports System.Text.RegularExpressions
#End Region

Namespace Core

	Public Class BuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Protected ReadOnly _builderLists As New Dictionary(Of String, BuilderList)
		Protected _name As String = ""
		Protected _comment As String = ""
		Protected _author As String = ""
		Protected _dateOfCreation As String = ""
		Protected _parameters As New Parameters
		Protected _commentChar As String
		Protected _endCommentChar As String
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New(ByVal commentChar As String)
			Me.New(commentChar, "")
		End Sub

		Public Sub New(ByVal startCommentChar As String, ByVal endCommentChar As String)
			_commentChar = startCommentChar
			_endCommentChar = endCommentChar
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Protected ReadOnly Property BuilderLists As Dictionary(Of String, BuilderList)
			Get
				Return _builderLists
			End Get
		End Property

		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				_name = value
			End Set
		End Property

		Public Property Comment() As String
			Get
				Return _comment
			End Get
			Set(ByVal value As String)
				_comment = value
			End Set
		End Property

		Public Property Author() As String
			Get
				Return _author
			End Get
			Set(ByVal value As String)
				_author = value
			End Set
		End Property

		Public Property DateOfCreation() As String
			Get
				Return _dateOfCreation
			End Get
			Set(ByVal value As String)
				_dateOfCreation = value
			End Set
		End Property

		Public ReadOnly Property Parameters() As Parameters
			Get
				Return _parameters
			End Get
		End Property
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		Public Function GetMetaData() As String

			If _commentChar Is Nothing Then Return ""

			Dim lineCommentChar = If(_endCommentChar = "", _commentChar, "")
			Dim sb = New StringBuilder

			If Not _endCommentChar = "" Then sb.AppendLine(_commentChar)

			sb.AppendLine(lineCommentChar & " ---")

			Dim header = GetHeader(lineCommentChar)

			If (header.Length = 0) AndAlso (String.IsNullOrEmpty(_comment)) Then
				Return ""
			End If

			If header.Length > 0 Then sb.Append(header.ToString)

			If Not String.IsNullOrEmpty(_comment) Then
				sb.AppendLine(String.Format("{0} Comment:", lineCommentChar))
				For Each s In Regex.Split(_comment, vbCrLf)
					sb.AppendLine(String.Format("{0} {1}", lineCommentChar, s))
				Next s
				sb.AppendLine(String.Format("{0} ---", lineCommentChar))
			End If

			If Not _endCommentChar = "" Then sb.AppendLine(_endCommentChar)
			sb.AppendLine()

			Return sb.ToString()
		End Function

		Private Function GetHeader(ByVal lineCommentChar As String) As String

			Dim pattern = String.Concat(lineCommentChar, " {0}: {1}")
			Dim sb = New StringBuilder

			If Not String.IsNullOrEmpty(_name) Then
				sb.AppendLine(String.Format(pattern, "Name", _name))
			End If

				If Not String.IsNullOrEmpty(_author) Then
				sb.AppendLine(String.Format(pattern, "Author", _author))
			End If

			If Not String.IsNullOrEmpty(_dateOfCreation) Then
				sb.AppendLine(String.Format(pattern, "Date of creation", _dateOfCreation))
			End If

			If sb.Length > 0 Then sb.AppendLine(lineCommentChar & " ---")

			Return sb.ToString
		End Function

		Public Function ResolveCommandText(ByVal cmd As IDbCommand) As String
			Dim query = cmd.CommandText
			For Each p As IDbDataParameter In cmd.Parameters
				query = query.Replace(p.ParameterName, p.Value.ToString)
			Next p

			Return query
		End Function

		Public Overrides Function ToString() As String
			Return GetMetaData()
		End Function

    Public sub ToClipboard
      My.Computer.Clipboard.SetText(me.ToString)
    End sub
#End Region

	End Class

End Namespace