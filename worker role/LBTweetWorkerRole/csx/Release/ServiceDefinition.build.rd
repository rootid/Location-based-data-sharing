<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="LBTweetWorkerRole" generation="1" functional="0" release="0" Id="aa158243-1595-450e-a101-849f66ca83a1" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="LBTweetWorkerRoleGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="LBTweetWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/LBTweetWorkerRole/LBTweetWorkerRoleGroup/MapLBTweetWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="LBTweetWorkerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/LBTweetWorkerRole/LBTweetWorkerRoleGroup/MapLBTweetWorkerInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapLBTweetWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/LBTweetWorkerRole/LBTweetWorkerRoleGroup/LBTweetWorker/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapLBTweetWorkerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/LBTweetWorkerRole/LBTweetWorkerRoleGroup/LBTweetWorkerInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="LBTweetWorker" generation="1" functional="0" release="0" software="D:\MS\2nd_Sem\Modern_Networking_Concepts\project\LBTweetWorkerRole\LBTweetWorkerRole\csx\Release\roles\LBTweetWorker" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;LBTweetWorker&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;LBTweetWorker&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/LBTweetWorkerRole/LBTweetWorkerRoleGroup/LBTweetWorkerInstances" />
            <sCSPolicyFaultDomainMoniker name="/LBTweetWorkerRole/LBTweetWorkerRoleGroup/LBTweetWorkerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="LBTweetWorkerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="LBTweetWorkerInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>