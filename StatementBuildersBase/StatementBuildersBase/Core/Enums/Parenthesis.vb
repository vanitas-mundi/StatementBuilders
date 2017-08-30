Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core.Enums

	Public Enum Parenthesis
		'''<summary>Keine weitere Klammersetzung</summary>
		None = 0
		'''<summary>Runde Klammer auf</summary>
		LeftParenthesis = 1
		'''<summary>Runde Klammer zu</summary>
		RightParenthesis = 2
	End Enum

End Namespace
