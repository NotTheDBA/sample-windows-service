# sample-windows-service

This is a project example I created to demonstrate the technique of having a windows service listener that 
polls a database for queued transaction records, then uploads the transaction data to a web API of a third-
party service.

The code is non-functional, but was desinged to be quickly converted to a functional implementation with
minimal effort.

Dependancies:

This project depends on the Microsoft Visual Studio 2017 Installer Projects plugin.  
https://marketplace.visualstudio.com/items?itemName=VisualStudioProductTeam.MicrosoftVisualStudio2017InstallerProjects

This project also requires read permissions to the Registery, you can apply them using the instructions
provided below 
(source: https://social.msdn.microsoft.com/Forums/windowsdesktop/en-US/00a043ae-9ea1-4a55-8b7c-d088a4b08f09/how-do-i-create-an-event-log-source-under-vista?forum=windowsgeneraldevelopmentissues)

	1)	Start -> Run -> regedit.exe
	2)	Navigate to My Computer > HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\EventLog
	3)	Right click this key, select Permissions, and grant the ASPNET account read/write permissions.
	4)	Navigate to My Computer > HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\EventLog\Security
	5)	Right click this key, select Permissions, and grant the ASPNET account read permissions as described above. 
		Note that for any other "inaccessible" logs, you'll also need to grant read access, as  permissions have been set to 
		not inherity from the parent key.
	6)	Cause the code line that creates the event source to be executed (EventLog.CreateEventSource())

For details on how to create and use a service installer package, please see :
https://www.youtube.com/watch?v=cp2aFNtcZfk