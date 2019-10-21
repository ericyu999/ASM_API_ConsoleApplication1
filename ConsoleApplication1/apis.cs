using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ASM.Sequoia.Api.Contracts;
using ASM.Sequoia.Server.Client;
using ASM.Sequoia.Server.Client.ApiService;
using ASM.Sequoia.Api.Contracts.DataContracts.Response;
using ASM.Sequoia.Api.Contracts.ServiceContracts;
using ASM.Sequoia.Api.Contracts.DataContracts.Request;
using ASM.Sequoia.Api;
using ASM.Sequoia.Api.Contracts.DataContracts.CallBacks;
using ASM.Sequoia.Service.Contracts;
using ASM.Sequoia.Service.Contracts.JobService.DataContracts;
using ASM.Sequoia.Service.Contracts.JobService.ServiceContracts;
using ASM.Sequoia.Server.Client.JobService;
using ASM.Sequoia.Common.Domain.Factories;
using ASM.Sequoia.Common.Domain.Entities.Job;
using ASM.Sequoia.Service.Contracts.JobService.Dto;

namespace ConsoleApplication1
{
    public class apis
    {
        private static ISequoiaApiServiceCallback m_SequoiaApiServiceCallback;
        private static IJobService m_jobService;
        private static IDomainEntitiesProvider m_DomainEntitiesProvider;
        public static void login()
        {
            
            try
            {
                //////////////////////////////////////////////////
                //DuplexChannelFactory<ISequoiaApiDualChannelService> factory =
                // new DuplexChannelFactory<ISequoiaApiDualChannelService>(m_SequoiaApiServiceCallback,
                // "endPointApiNetTcpWithCallback");



                ////////////////////////////////////////////////////////////////////
                ApiServiceClient apiSC = new ApiServiceClient("endPointApiNetTcpWithCallback");
                //bool logon = apiSC.Logon("api", "api");
                
                //Console.WriteLine(logon ? "Logged in!" : "Did not authenticate");

                //if (logon)
                //{
                    //get job by reference
                DuplexChannelFactory<ISequoiaApiDualChannelService> factory = apiSC.GetChannelFactory();
                ISequoiaApiDualChannelService channel = factory.CreateChannel();
                SequoiaLogonResponse result = channel.Logon(new SequoiaLogonRequest {
                    UserName = "api",
                    Password = "api"
                });
                Console.WriteLine(result.Authenticated  ? "Logged in!" : "Did not authenticate");
                if (result.Authenticated)
                {
                    //Console.WriteLine(channel.GetApiVersion().ReturnValue);
                    m_SequoiaApiServiceCallback = new SequoiaApiServiceCallback();

                    ChannelFactory<IJobService> factory_job = new ChannelFactory<IJobService>("endPointJobService");
                    IJobService job_channel = factory_job.CreateChannel();
                    GetJobByReferenceRequest req_job1 = new GetJobByReferenceRequest();
                    GetJobByReferenceRequest req_job2 = new GetJobByReferenceRequest();
                    GetJobByReferenceRequest req_job3 = new GetJobByReferenceRequest();
                    ////////////////////////////////////////////
                    req_job1.Reference = "AE015732";
                    GetJobByReferenceResponse jobResp = job_channel.GetJobByReference(req_job1);
                    int hash_code = job_channel.GetHashCode();
                    Console.WriteLine("Hash Code : " + hash_code.ToString());

                    Console.WriteLine("############JOB Details For " + req_job1.Reference + "############");

                    Console.WriteLine("Reference : " + jobResp.JobHeader.JobReference);
                    Console.WriteLine("Mode : " + jobResp.JobHeader.Mode.ToString());
                    Console.WriteLine("Type : " + jobResp.JobHeader.Type.ToString());
                    Console.WriteLine("CustomerAccountId : " + jobResp.JobHeader.CustomerAccountId.ToString());
                    Console.WriteLine("Created By : " + jobResp.JobHeader.CreatedBy.ToString());
                    Console.WriteLine("Created On : " + jobResp.JobHeader.CreatedOn.ToString());
                    Console.WriteLine("Modified By : " + jobResp.JobHeader.ModifiedBy.ToString());
                    Console.WriteLine("Modified On : " + jobResp.JobHeader.ModifiedOn.ToString());
                    Console.WriteLine("##########################################");
                    ////////////////////////////////////////////////////////////////////////////
                    req_job2.Reference = "OI030013";
                    GetJobByReferenceResponse jobResp2 = job_channel.GetJobByReference(req_job2);

                    Console.WriteLine("############JOB Details For " + req_job2.Reference + "############");

                    Console.WriteLine("Reference : " + jobResp2.JobHeader.JobReference);
                    Console.WriteLine("Mode : " + jobResp2.JobHeader.Mode.ToString());
                    Console.WriteLine("Type : " + jobResp2.JobHeader.Type.ToString());
                    Console.WriteLine("CustomerAccountId : " + jobResp2.JobHeader.CustomerAccountId.ToString());
                    Console.WriteLine("Created By : " + jobResp2.JobHeader.CreatedBy.ToString());
                    Console.WriteLine("Created On : " + jobResp2.JobHeader.CreatedOn.ToString());
                    Console.WriteLine("Modified By : " + jobResp2.JobHeader.ModifiedBy.ToString());
                    Console.WriteLine("Modified On : " + jobResp2.JobHeader.ModifiedOn.ToString());
                    Console.WriteLine("##########################################");
                    ////////////////////////////////////////////////////////////////////////////
                    req_job3.Reference = "AI030301";
                    GetJobByReferenceResponse jobResp3 = job_channel.GetJobByReference(req_job3);

                    Console.WriteLine("############JOB Details For " + req_job3.Reference + "############");

                    Console.WriteLine("Reference : " + jobResp3.JobHeader.JobReference);
                    Console.WriteLine("Mode : " + jobResp3.JobHeader.Mode.ToString());
                    Console.WriteLine("Type : " + jobResp3.JobHeader.Type.ToString());
                    Console.WriteLine("CustomerAccountId : " + jobResp3.JobHeader.CustomerAccountId.ToString());
                    Console.WriteLine("Created By : " + jobResp3.JobHeader.CreatedBy.ToString());
                    Console.WriteLine("Created On : " + jobResp3.JobHeader.CreatedOn.ToString());
                    Console.WriteLine("Modified By : " + jobResp3.JobHeader.ModifiedBy.ToString());
                    Console.WriteLine("Modified On : " + jobResp3.JobHeader.ModifiedOn.ToString());
                    Console.WriteLine("##########################################");
                    ////////////////////////////////////////////////////////////////////////////
                }
                //    GetJobByReferenceResponse jobResp = m_jobService.GetJobByReference(new GetJobByReferenceRequest
                //    {
                //        Reference = "TESTJOB"
                //    });

                //}

            }
            catch (Exception ex)
            {           
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
        public class SequoiaApiServiceCallback : ISequoiaApiServiceCallback
        {
            public void PublishMessage(PublishedMessage message)
            {
                Console.WriteLine(message);
            }
        }

    }
}
