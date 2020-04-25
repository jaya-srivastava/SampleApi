a) make sure CorsSetting coming from web.config
   CorsAllowedUrl

b) make sure httpCompression is working 

c) In <system.webServer> section of web.config
<system.webServer>
    <directoryBrowse enabled="false"/>
    <validation validateIntegratedModeConfiguration="false" />
    <modules runAllManagedModulesForAllRequests="false">
      <remove name="FormsAuthentication" />
    </modules>

d) In aspnet.config file : add this section if not exist
	
 <system.web>
    <applicationPool 
        maxConcurrentRequestsPerCPU="5000"
        maxConcurrentThreadsPerCPU="0" 
        requestQueueLimit="5000" />
  </system.web>


e) in Machine.config file : 

<system.web>
<processModel autoConfig="false" maxWorkerThreads="100" maxIoThreads="100" minWorkerThreads="50" />
<httpRuntime minFreeThreads="176" minLocalRequestFreeThreads="152"/>
</system.web>

f) in Web.config - this section after allowing override in machine.config - or just add in machine.config
<system.net>
    <connectionManagement>
      <add address="*" maxconnection="65535"/>
	  
	  <add address = "http://www.contoso.com" maxconnection = "4" />
      <add address = "*" maxconnection = "2" />
    </connectionManagement>
  </system.net>