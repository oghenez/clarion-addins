<?xml version="1.0"?>
<doc>
<members>
<member name="T:UacHelpers.UserAccountControl" decl="false" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="10">
<summary>
Provides facilities for enabling and disabling User Account Control (UAC),
determining elevation and virtualization status, and launching a process
under elevated credentials.
</summary>
<remarks>
Note that there's a delicate scenario where the registry key has already been
changed, but the user has not logged off yet so the token hasn't been filtered.
In that case, we will think that UAC is on but the user is not an admin (because
the token is not a split token).
</remarks>
</member>
<member name="P:UacHelpers.UserAccountControl.IsUserAdmin" decl="false" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="24">
<summary>
Returns <b>true</b> if the current user has administrator privileges.
</summary>
<remarks>
If UAC is on, then this property will return <b>true</b> even if the
current process is not running elevated.  If UAC is off, then this
property will return <b>true</b> if the user is part of the built-in
<i>Administrators</i> group.
</remarks>
</member>
<member name="P:UacHelpers.UserAccountControl.IsUacEnabled" decl="false" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="38">
<summary>
Returns <b>true</b> if User Account Control (UAC) is enabled on
this machine.
</summary>
<remarks>
This value is obtained by checking the LUA registry key.  It is possible
that the user has not restarted the machine after enabling/disabling UAC.
In that case, the value of the registry key does not reflect the true state
of affairs.  It is possible to devise a custom solution that would provide
a mechanism for tracking whether a restart occurred since UAC settings were
changed (using the RunOnce mechanism, temporary files, or volatile registry keys).
</remarks>
</member>
<member name="P:UacHelpers.UserAccountControl.IsCurrentProcessVirtualized" decl="false" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="55">
<summary>
Returns <b>true</b> if the current process is using UAC virtualization.
</summary>
<remarks>
Under UAC virtualization, file system and registry accesses to specific
locations performed by an application are redirected to provide backwards-
compatibility.  64-bit applications or applications that have an associated
manifest do not enjoy UAC virtualization because they are assumed to be
compatible with Vista and UAC.
</remarks>
</member>
<member name="P:UacHelpers.UserAccountControl.IsCurrentProcessElevated" decl="false" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="70">
<summary>
Returns <b>true</b> if the current process is elevated, i.e. if the process
went through an elevation consent phase.
</summary>
<remarks>
This property will return <b>false</b> if UAC is disabled and the process
is running as admin.  It only determines whether the process went through
the elevation procedure.
</remarks>
</member>
<member name="M:UacHelpers.UserAccountControl.DisableUac" decl="true" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="84">
<summary>
Disables User Account Control by changing the LUA registry key.
The changes do not have effect until the system is restarted.
</summary>
</member>
<member name="M:UacHelpers.UserAccountControl.DisableUacAndRestartWindows" decl="true" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="90">
<summary>
Disables User Account Control and restarts the system.
</summary>
</member>
<member name="M:UacHelpers.UserAccountControl.EnableUac" decl="true" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="95">
<summary>
Enables User Account Control by changing the LUA registry key.
The changes do not have effect until the system is restarted.
</summary>
</member>
<member name="M:UacHelpers.UserAccountControl.EnableUacAndRestartWindows" decl="true" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="101">
<summary>
Enables User Account Control and restarts the system.
</summary>
</member>
<member name="M:UacHelpers.UserAccountControl.CreateProcessAsAdmin(System.String,System.String)" decl="true" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="106">
<summary>
Creates a process under the elevated token, regardless of UAC settings
or the manifest associated with that process.
</summary>
<param name="exePath">The path to the executable file.</param>
<param name="arguments">The command-line arguments to pass to the process.</param>
<returns>A <see cref="T:System.Diagnostics.Process"/> object representing the newly created process.</returns>
</member>
<member name="M:UacHelpers.UserAccountControl.CreateProcessAsStandardUser(System.String,System.String)" decl="true" source="c:\dev\clarionedgeaddins\clarion-addins\uachelpers\uachelpers.cpplibrary\useraccountcontrol.h" line="115">
<summary>
Creates a process under the standard user if the current process is elevated.  The identity
of the standard user is determined by retrieving the user token of the currently running Explorer
</summary>
<param name="exePath">The path to the executable file.</param>
<param name="arguments">The command-line arguments to pass to the process.</param>
<returns>A <see cref="T:System.Diagnostics.Process"/> object representing the newly created process.</returns>
</member>
</members>
</doc>
