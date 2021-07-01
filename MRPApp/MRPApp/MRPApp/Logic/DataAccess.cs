using MRPApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
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

        internal static List<Report> GetReportDatas(string startDate, string endDate, string plantCode)
        {
            var connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            var list = new List<Report>();
            var lastObj = new Model.Report();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var sqlQuery = $@"SELECT sch.SchIdx, sch.PlantCode, sch.SchAmount, prc.PrcDate,
	                                prc.PrcOkAmount, prc.PrcFailAmount
	                                FROM Schedules AS sch 
	                                INNER JOIN (
		                                SELECT Smr.SchIdx, smr.PrcDate, 
                                        SUM(Smr.PrcOK) AS PrcOkAmount, SUM(Smr.PrcFail) AS PrcFailAmount
			                            FROM (
			                                SELECT p.SchIdx, p.PrcDate, 
			                                CASE p.prcResult WHEN 1 THEN 1 ELSE 0 END AS PrcOK,
			                                CASE p.prcResult WHEN 0 THEN 1 ELSE 0 END AS PrcFail
			                                FROM Process AS p
			                                ) AS Smr
		                                GROUP BY Smr.SchIdx, Smr.PrcDate
	                                    ) AS prc
	                                ON sch.SchIdx = prc.SchIdx
	                                WHERE sch.PlantCode = '{plantCode}'
	                                AND prc.PrcDate BETWEEN '{startDate}' AND '{endDate}'";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Report tmp = new Report
                    {
                        SchIdx = int.Parse(reader["SchIdx"].ToString()),
                        PlantCode = reader["PlantCode"].ToString(),
                        PrcDate = DateTime.Parse(reader["PrcDate"].ToString()),
                        SchAmount = int.Parse(reader["SchAmount"].ToString()),
                        PrcOkAmount = int.Parse(reader["PrcOkAmount"].ToString()),
                        PrcFailAmount = int.Parse(reader["PrcFailAmount"].ToString()),
                    };
                    list.Add(tmp);
                    lastObj = tmp;
                }

                var DtStart = DateTime.Parse(startDate);
                var DtEnd = DateTime.Parse(endDate);
                var DtCurrent = DtStart;

                while (DtCurrent < DtEnd)
                {
                    var count = list.Where(c => c.PrcDate.Equals(DtCurrent)).Count();
                    if (count == 0)
                    {
                        var tmp = new Report
                        {
                            SchIdx = lastObj.SchIdx,
                            PlantCode = lastObj.PlantCode,
                            PrcDate = DtCurrent,
                            SchAmount = 0,
                            PrcOkAmount = 0,
                            PrcFailAmount = 0
                        };
                        list.Add(tmp);
                    }
                    DtCurrent = DtCurrent.AddDays(1);
                }
                list.Sort((reportA, reportB) => reportA.PrcDate.CompareTo(reportB.PrcDate));
                return list;
            }
        }
    }
}
