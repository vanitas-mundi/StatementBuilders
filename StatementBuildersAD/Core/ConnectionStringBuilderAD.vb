Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports "
Imports System.Data.Common
#End Region

Namespace Core

	''' <summary>
	''' Provides a simple way to create and manage the contents of
	''' connection strings used by the AdConnectionString class.
	''' </summary>
	Public Class ConnectionStringBuilderAD

		Inherits DbConnectionStringBuilder

#Region " --------------->> Enumerationen der Klasse "
		Private Enum KeyWords
			Provider = 0
			DomainController = 1
			DomainName = 2
			Port = 3
			UserId = 4
			Password = 5
			Delegation = 6
			FastBind = 7
			ReadOnlyServer = 8
			Sealing = 9
			Secure = 10
			SecureSocketsLayer = 11
			ServerBind = 12
			Signing = 13
		End Enum
#End Region '{Enumerationen der Klasse}

#Region " --------------->> Eigenschaften der Klasse "
		Private _keyWords As New Dictionary(Of KeyWords, String())
		Private _domainName As String
		Private _domainController As String
#End Region '{Eigenschaften der Klasse}

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New(ByVal domainName As String, ByVal domainController As String)
			Initialize()
			Me.Provider = My.Settings.AdoProviderNameAD
			Me.DomainName = domainName
			Me.DomainController = domainController
			Me.Secure = True
			Me.ServerBind = True
		End Sub

		Public Sub New(ByVal connectionString As String)
			Initialize()
			Me.ConnectionString = connectionString
		End Sub
#End Region  '{Konstruktor und Destruktor der Klasse}

#Region " --------------->> Zugriffsmethoden der Klasse "
		'''<summary>Gets the name of the provider for the AdConnectionStringBuilder.</summary>
		'''<remarks>
		'''May be one of but not excluded to the following values:
		'''<list>
		'''<item>LDAP: Active Directory (default).</item>
		'''<item>GC: Active Directory Global Catalog.</item>
		'''WinNT: Windows NT 4 Domain Controllers and local SAM databases.
		'''<item>NDS: Novell Directory Services.</item>
		'''NWCOMPAT: Older Novell Services.
		'''<item>IIS: Internet Information Services metabase.</item>
		'''<item>Ads: Enumerate installed directory providers.</item>
		'''</list>
		'''</remarks>
		Public Property Provider As String
			Get
				Return GetKeyValue(KeyWords.Provider, "LDAP").ToString.Trim.ToUpper
			End Get
			Set(ByVal value As String)
				AddToConnection(KeyWords.Provider, value)
			End Set
		End Property

		'''<summary>Gets or sets the name of the domain to connect.</summary>
		'''<returns>Returns the domain name if available otherwise returns the default domain name.</returns>
		Public Property DomainName As String
			Get
				Dim value = GetKeyValue(KeyWords.DomainName, _domainName).ToString
				Return value.Trim.Replace("localhost", _domainName)
			End Get
			Set(ByVal value As String)
				_domainName = value
				AddToConnection(KeyWords.DomainName, value)
			End Set
		End Property

		'''<summary>Gets or sets the name of the specific domain controller to connect.</summary>
		'''<returns>
		'''Returns the domain controller if available otherwise returns the default domain
		'''controller for the specified domain name.
		'''</returns>
		'''<remarks>
		'''If ServerBind is specified and a server name is not supplied for the
		'''Domain Controller then a default domain controller will automatically
		'''be found. Because the system detects errors in domain controllers and
		'''reconnects as needed it is recommended to use ServerBind without
		'''specifying a domain controller.
		'''</remarks>
		Public Property DomainController As String
			Get
				Dim defaultValue = If(Me.ServerBind, _domainController, String.Empty)
				Dim value = GetKeyValue(KeyWords.DomainController, defaultValue).ToString
				Return value.Trim.Replace("localhost", _domainController)
			End Get
			Set(ByVal value As String)
				_domainController = value
				AddToConnection(KeyWords.DomainController, value)
			End Set
		End Property

		'''<summary>Gets or sets the port number to connect.</summary>
		'''<returns>
		'''Returns the default port number for LDAP and GC providers.
		'''If the provider is not recognized and a port number has
		'''not been provided a negative value is returned.
		'''</returns>
		Public Property PortNumber As Int32
			Get
				Return Convert.ToInt32(GetKeyValue(KeyWords.Port, GetPortDefaultValue))
			End Get
			Set(ByVal value As Int32)
				AddToConnection(KeyWords.Port, value)
			End Set
		End Property

		Private Function GetPortDefaultValue() As Int32

			Select Case Me.Provider
				Case "LDAP"
					Return Convert.ToInt32(If(Me.SecureSocketsLayer, 636, 389))
				Case "GC"
					Return Convert.ToInt32(If(Me.SecureSocketsLayer, 3269, 3268))
				Case Else
					Return -1
			End Select
		End Function

		'''<summary>Gets or sets the name of the user account to use for security context.</summary>
		Public Property UserId As String
			Get
				Return GetKeyValue(KeyWords.UserId, Nothing).ToString
			End Get
			Set(ByVal value As String)
				AddToConnection(KeyWords.UserId, value)
			End Set
		End Property

		'''<summary>Gets or sets the password for the user account to use for security context.</summary>
		Public Property Password As String
			Get
				Return GetKeyValue(KeyWords.Password, Nothing).ToString
			End Get
			Set(ByVal value As String)
				AddToConnection(KeyWords.Password, value)
			End Set
		End Property

		'''<summary>Gets or sets a value indicating that the security context from another network may be used.</summary>
		'''<remarks>Must be used with the Secure flag.</remarks>
		Public Property Delegation As Boolean
			Get
				Return GetBooleanKeyValue(KeyWords.Delegation)
			End Get
			Set(ByVal value As Boolean)
				AddToConnection(KeyWords.Delegation, value)
			End Set
		End Property

		'''<summary>Gets or sets a value that disables querying for the object class.</summary>
		'''<remarks>
		'''Increases performance by restricting queries to one operation instead of two.
		'''Use with caution as Fast Binding limits the capabilities of the system.
		'''</remarks>
		Public Property FastBind As Boolean
			Get
				Return GetBooleanKeyValue(KeyWords.FastBind)
			End Get
			Set(ByVal value As Boolean)
				AddToConnection(KeyWords.FastBind, value)
			End Set
		End Property

		'''<summary>Gets or sets a value that establishes a read-only connection.</summary>
		'''<remarks>
		'''Currently has no impact on Microsoft AD.
		'''</remarks>
		Public Property ReadOnlyServer As Boolean
			Get
				Return GetBooleanKeyValue(KeyWords.ReadOnlyServer)
			End Get
			Set(ByVal value As Boolean)
				AddToConnection(KeyWords.ReadOnlyServer, value)
			End Set
		End Property

		'''<summary>Gets or sets a value encrypting secure trafic.</summary>
		'''<remarks>
		'''Not supported with all authentication protocols. Is supported
		'''with Kerberos and NTLM on Windows Server 2005 and greater.
		'''</remarks>
		Public Property Sealing As Boolean
			Get
				Return GetBooleanKeyValue(KeyWords.Sealing)
			End Get
			Set(ByVal value As Boolean)
				AddToConnection(KeyWords.Sealing, value)
			End Set
		End Property

		''' <summary>
		''' Gets or sets a value indicating to connect with Windows Security Support
		''' Provider Interface (SSPI).
		''' </summary>
		''' <remarks>
		''' Ensures that credentials are stored in encrypted text. Supports connections
		''' with both explicit credentials and the current windows security context.
		''' To encrypt traffic use with Sealing and to ensure traffic is not tampered
		''' with use with Signing.
		''' </remarks>
		Public Property Secure As Boolean
			Get
				Return GetBooleanKeyValue(KeyWords.Secure)
			End Get
			Set(ByVal value As Boolean)
				AddToConnection(KeyWords.Secure, value)
			End Set
		End Property

		''' <summary>
		''' Gets or sets a value that specifies that SSL / TLS Protocol will be used to
		''' encrypt the traffic.
		''' </summary>
		''' <remarks>
		''' May be used with Secure flag but not Sealing or Signing flags.
		''' </remarks>
		Public Property SecureSocketsLayer As Boolean
			Get
				Return GetBooleanKeyValue(KeyWords.SecureSocketsLayer)
			End Get
			Set(ByVal value As Boolean)
				AddToConnection(KeyWords.SecureSocketsLayer, value)
			End Set
		End Property

		'''<summary>Gets or sets a value that indicates that an exact server bind is being provided.</summary>
		Public Property ServerBind As Boolean
			Get
				Return GetBooleanKeyValue(KeyWords.ServerBind)
			End Get
			Set(ByVal value As Boolean)
				AddToConnection(KeyWords.ServerBind, value)
			End Set
		End Property

		'''<summary>Gets or sets a value indicating to sign secure traffic to determine if the data has been tampered with.</summary>
		'''<remarks>Requires a Secure connection.</remarks>
		Public Property Signing As Boolean
			Get
				Return GetBooleanKeyValue(KeyWords.Signing)
			End Get
			Set(ByVal value As Boolean)
				AddToConnection(KeyWords.Signing, value)
			End Set
		End Property
#End Region '{Zugriffsmethoden der Klasse}

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region '{Ereignismethoden der Klasse}

#Region " --------------->> Private Methoden der Klasse "
		Private Sub Initialize()
			_keyWords.Add(KeyWords.Provider, New String() {"Provider"})

			_keyWords.Add(KeyWords.DomainController, New String() _
			{"Domain Controller", "Initial Catalog", "Database"})

			_keyWords.Add(KeyWords.DomainName, New String() _
			{"Domain Name", "Data Source", "Server", "Address", "Addr", "Network Address"})

			_keyWords.Add(KeyWords.UserId, New String() {"User Id", "UID"})
			_keyWords.Add(KeyWords.Password, New String() {"Password", "Pwd"})
			_keyWords.Add(KeyWords.Delegation, New String() {"Delegate"})
			_keyWords.Add(KeyWords.FastBind, New String() {"Fast Bind", "FastBind"})
			_keyWords.Add(KeyWords.ReadOnlyServer, New String() {"ReadOnlyServer"})
			_keyWords.Add(KeyWords.Sealing, New String() {"Sealing"})

			_keyWords.Add(KeyWords.Secure, New String() _
			{"Secure", "Integrated Security", "Trusted_Connection"})

			_keyWords.Add(KeyWords.SecureSocketsLayer, New String() _
			{"SecureSocketsLayer", "TrustServerCertificate"})

			_keyWords.Add(KeyWords.ServerBind, New String() {"ServerBind"})
			_keyWords.Add(KeyWords.Signing, New String() {"Signing"})
			_keyWords.Add(KeyWords.Port, New String() {"Port", "Port Number"})
		End Sub

		Private Function GetBooleanKeyValue(ByVal keyWord As KeyWords) As Boolean
			Select Case GetKeyValue(keyWord, False).ToString.Trim.ToUpper
				Case "YES", "TRUE", "ON"
					Return True
				Case Else
					Return False
			End Select
		End Function

		Private Function GetKeyValue(ByVal keyWord As KeyWords, ByVal [default] As Object) As Object
			Dim value As Object = Nothing

			For Each key In _keyWords.Item(keyWord)
				If MyBase.TryGetValue(key, value) Then Return value
			Next key

			Return [default]
		End Function

		Private Sub AddToConnection(ByVal keyWord As KeyWords, ByVal value As Object)
			If String.IsNullOrWhiteSpace(value.ToString) Then
				_keyWords.Item(keyWord).ToList.ForEach(Sub(s) MyBase.Remove(s))
			Else
				MyBase.Add(_keyWords.Item(keyWord)(0), value)
			End If
		End Sub
#End Region  '{Private Methoden der Klasse}

#Region " --------------->> Öffentliche Methoden der Klasse "
#End Region  '{Öffentliche Methoden der Klasse}

	End Class
End Namespace

