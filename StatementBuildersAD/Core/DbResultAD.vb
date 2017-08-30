Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports System.Data.OleDb
Imports SSP.Data.StatementBuildersBase.Core
#End Region

Namespace Core

	Public Class DbResultAD

		Private Shared _instance As DbResultBase
		Private Shared _quotingRules As QuotingRulesAD
		Private Shared _defaultConnectionString As String

#Region " --------------->> Enumerationen der Klasse "
#End Region '{Enumerationen der Klasse}

#Region " --------------->> Eigenschaften der Klasse "
		Private Shared _escapeLetter As String() = New String() _
		{"\", ",", "#", "+", "<", ">", ";", """", "=", "'", "`", "´"}
#End Region '{Eigenschaften der Klasse}

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Shared Sub New()
			_quotingRules = New QuotingRulesAD
		End Sub
		Private Sub New()
		End Sub

		Public Shared Sub Initialize()
			Initialize("")
		End Sub

		'''<summary>Initialisiert das DbResult-Objekt mit einem Standard-ConnectionString.</summary>
		''' <param name="defaultConnectionString"></param>
		Public Shared Sub Initialize(ByVal defaultConnectionString As String)

			_defaultConnectionString = defaultConnectionString
			_instance = DbResultBase.CreateDbResultBaseObject _
			(OleDbFactory.Instance, defaultConnectionString _
			, My.Settings.GetLastInsertIdStatementAD)
		End Sub
#End Region '{Konstruktor und Destruktor der Klasse}

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public Shared ReadOnly Property Instance As DbResultBase
			Get
				Return _instance
			End Get
		End Property

		'''<summary>Liefert den Standard-ConnectionString.</summary>
		Public Shared ReadOnly Property DefaultConnectionString As String
			Get
				Return _defaultConnectionString
			End Get
		End Property
#End Region '{Zugriffsmethoden der Klasse}

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region '{Ereignismethoden der Klasse}

#Region " --------------->> Private Methoden der Klasse "
		Private Shared Function EscapeEmptyStart(ByVal s As String) As String
			Dim lEnd = s.Length
			s = s.TrimStart
			Dim lStart = s.Length

			For i = lStart To lEnd
				s = "\ " & s
			Next
			Return s
		End Function

		Private Shared Function EscapeEmptyEnd(ByVal s As String) As String
			Dim lEnd = s.Length
			s = s.TrimEnd
			Dim lStart = s.Length

			For i = lStart To lEnd
				s &= "\ "
			Next
			Return s
		End Function
#End Region '{Private Methoden der Klasse}

#Region " --------------->> Öffentliche Methoden der Klasse "
		'''<summary>
		'''Maskiert Sonderzeichen mit einem voranstehendem "\"
		'''Bsp: Dampf, Hans => Dampf\, Hans
		'''</summary>
		Public Shared Function EscapeValue(ByVal value As String) As String

			_escapeLetter.ToList.ForEach(Sub(s) value = value.Replace(s, "\" & s))
			Return EscapeEmptyStart(EscapeEmptyEnd(value))
		End Function

		'''<summary>
		'''Demaskiert Sonderzeichen im einem voranstehendem "\"
		'''Bsp: Dampf\, Hans => Dampf, Hans
		'''</summary>
		Public Shared Function DeEscapeValue(ByVal value As String) As String
			_escapeLetter.ToList.ForEach(Sub(s) value = value.Replace("\" & s, s))
			Return value.Replace("\ ", " ")
		End Function

		'''<summary>Liefert ein QuotingRules-Objekt mit Parameter-Quotierungs-Regeln.</summary>
		Public Shared Function QuotingRules() As QuotingRulesAD
			Return _quotingRules
		End Function

		'''<summary>Maskiert einen String für eine MySQL-Abfrage.</summary>
		Public Shared Function ReplaceEscape(ByVal s As String) As String

			Throw New NotImplementedException
			'Return StringMethods.ReplaceEscapeMySql(s)
		End Function
#End Region '{Öffentliche Methoden der Klasse}

	End Class

End Namespace
