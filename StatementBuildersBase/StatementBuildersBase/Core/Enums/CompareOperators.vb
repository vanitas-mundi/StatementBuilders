Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core.Enums

	Public Enum CompareOperators
		'''<summary>entspricht (=).</summary>
		Equal = 0
		'''<summary>ist NULL (IS NULL).</summary>
		[IsNull] = 1
		'''<summary>ist nicht NULL (IS NOT NULL).</summary>
		[IsNotNull] = 2
		'''<summary>kleiner als (&lt;).</summary>
		LesserThan = 3
		'''<summary>größer als (&gt;).</summary>
		GreaterThan = 4
		'''<summary>kleiner gleich als (&lt;=).</summary>
		LesserEqualThan = 5
		'''<summary>größer gleich als (&gt;=).</summary>
		GreaterEqualThan = 6
		'''<summary>ungleich (&lt;=&gt;=).</summary>
		Unequal = 7
		'''<summary>wie (LIKE).</summary>
		[Like] = 8
		'''<summary>nicht wie (NOT LIKE).</summary>
		[NotLike] = 9
		'''<summary>ist einer von (IN(...)).</summary>
		[In] = 10
		'''<summary>ist keiner von (NOT IN(...)).</summary>
		[NotIn] = 11
	End Enum

End Namespace