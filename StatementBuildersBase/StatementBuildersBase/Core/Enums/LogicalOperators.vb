Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core.Enums

	Public Enum LogicalOperators
		'''<summary>Keine Verkn�pfung.</summary>
		[None] = 0
		'''<summary>Und-Verkn�pfung.</summary>
		[And] = 1
		'''<summary>Oder-Verkn�pfung.</summary>
		[Or] = 2
		'''<summary>Xor-Verkn�pfung.</summary>
		[Xor] = 3
		'''<summary>Nicht-Verkn�pfung.</summary>
		[Not] = 4
		'''<summary>Und-Nicht-Verkn�pfung.</summary>
		[AndNot] = 5
		'''<summary>Oder-Nicht-Verkn�pfung.</summary>
		[OrNot] = 6
		'''<summary>Xor-Nicht-Verkn�pfung.</summary>
		[XorNot] = 7
	End Enum

End Namespace