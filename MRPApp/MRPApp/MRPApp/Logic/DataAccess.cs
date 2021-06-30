using MRPApp.Model;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MRPApp.Logic
{
    public class DataAccess
    {
        public static List<Settings> GetSettings()
        {
            List<Settings> settings;

            using (MRPEntities ctx = new MRPEntities())
            {
                settings = ctx.Settings.ToList();
            }

            return settings;
        }

        public static int SetSettings(Settings item)
        {
            using (MRPEntities ctx = new MRPEntities())
            {
                ctx.Settings.AddOrUpdate(item);
                return ctx.SaveChanges(); // COMMIT
            }
        }

        public static List<Schedules> GetSchedules()
        {
            List<Schedules> schedules;

            using (MRPEntities ctx = new MRPEntities())
            {
                schedules = ctx.Schedules.ToList();
            }

            return schedules;
        }

        public static int SetSchedules(Schedules item)
        {
            using (MRPEntities ctx = new MRPEntities())
            {
                ctx.Schedules.AddOrUpdate(item);
                return ctx.SaveChanges(); // COMMIT
            }
        }

        public static List<Process> GetProcess()
        {
            List<Model.Process> process;

            using (MRPEntities ctx = new MRPEntities())
            {
                process = ctx.Process.ToList();
            }

            return process;
        }

        public static int SetProcess(Process item)
        {
            using (MRPEntities ctx = new MRPEntities())
            {
                ctx.Process.AddOrUpdate(item);
                return ctx.SaveChanges(); // COMMIT
            }
        }
    }
}
