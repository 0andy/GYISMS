using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using GYISMS.Controllers;
using Abp.Quartz;
using Abp.Threading;
using GYISMS.VisitTaskJobs;
using Quartz;

namespace GYISMS.Web.Host.Controllers
{
    public class HomeController : GYISMSControllerBase
    {
        private readonly INotificationPublisher _notificationPublisher;
        private readonly IQuartzScheduleJobManager _jobManager;

        public HomeController(INotificationPublisher notificationPublisher, IQuartzScheduleJobManager jobManager)
        {
            _notificationPublisher = notificationPublisher;
            _jobManager = jobManager;
        }

        public IActionResult Index()
        {
            //QuartzScheduleJobs();
            //return Redirect("/swagger");
            return Redirect("/gyadmin/index.html");
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: NotificationSeverity.Info,
                userIds: new[] { defaultTenantAdmin, hostAdmin }
            );

            return Content("Sent notification: " + message);
        }

        public async Task<ActionResult> ScheduleJob()
        {
            await _jobManager.ScheduleAsync<VisitTaskStatusJob>(job =>
            {
                job.WithIdentity("VisitTaskStatusJob", "TaskGroup").WithDescription("A job to update task status.");
            },
            trigger =>
            {
                trigger//.StartAt(new DateTimeOffset(startTime))
                .StartNow()//һ������scheduler��������Ч
                .WithCronSchedule("0 0 2 * * ?")//ÿ���賿2��ִ��
                .Build();
                //.StartNow()//һ������scheduler��������Ч
                /*.WithSimpleSchedule(schedule =>//ʹ��SimpleTrigger
                {
                    schedule.RepeatForever()  //һֱִ�У����ڵ��ϲ�ͣЪ
                    .WithIntervalInHours(24)  // ÿ��24Сʱִ��һ��
                    //.WithIntervalInSeconds(5) // ÿ��5��ִ��һ��
                    .Build();
                });*/
            });

            await _jobManager.ScheduleAsync<SendTaskOverdueMsgJob>(job =>
            {
                job.WithIdentity("SendTaskOverdueMsgJob", "TaskGroup").WithDescription("A job to send msg.");
            },
            trigger =>
            {
                trigger//.StartAt(new DateTimeOffset(startTime))
                .StartNow()//һ������scheduler��������Ч
                .WithCronSchedule("0 0 9 * * ?")//ÿ������9��ִ��
                .Build();
            });

            return Content("OK, scheduled!");
        }


        /// <summary>
        /// ����jobs
        /// </summary>
        private void QuartzScheduleJobs()
        {
            //var startTime = DateTime.Today.AddHours(2);
            //if (startTime < DateTime.Now)
            //{
            //    startTime.AddDays(1);
            //}
            AsyncHelper.RunSync(() => _jobManager.ScheduleAsync<VisitTaskStatusJob>(job =>
            {
                job.WithIdentity("VisitTaskStatusJob", "TaskGroup").WithDescription("A job to update task status.");
            },
            trigger =>
            {
                trigger//.StartAt(new DateTimeOffset(startTime))
                .StartNow()//һ������scheduler��������Ч
                .WithCronSchedule("0 35 14 * * ?")//ÿ���賿2��ִ��
                .Build();
                //.StartNow()//һ������scheduler��������Ч
                /*.WithSimpleSchedule(schedule =>//ʹ��SimpleTrigger
                {
                    schedule.RepeatForever()  //һֱִ�У����ڵ��ϲ�ͣЪ
                    .WithIntervalInHours(24)  // ÿ��24Сʱִ��һ��
                    //.WithIntervalInSeconds(5) // ÿ��5��ִ��һ��
                    .Build();
                });*/
            }));
        }
    }
}
