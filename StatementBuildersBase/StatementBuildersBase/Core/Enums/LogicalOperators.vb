Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core.Enums

	Public Enum LogicalOperators
		'''<summary>Keine Verknüpfung.</summary>
		[None] = 0
		'''<summary>Und-Verknüpfung.</summary>
		[And] = 1
		'''<summary>Oder-Verknüpfung.</summary>
		[Or] = 2
		'''<summary>Xor-Verknüpfung.</summary>
		[Xor] = 3
		'''<summary>Nicht-Verknüpfung.</summary>
		[Not] = 4
		'''<summary>Und-Nicht-Verknüpfung.</summary>
		[AndNot] = 5
		'''<summary>Oder-Nicht-Verknüpfung.</summary>
		[OrNot] = 6
		'''<summary>Xor-Nicht-Verknüpfung.</summary>
		[XorNot] = 7
	End Enum

End Namespace