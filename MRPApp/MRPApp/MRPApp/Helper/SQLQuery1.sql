SELECT Smr.SchIdx, smr.PrcDate, SUM(Smr.PrcOK) AS Success, SUM(Smr.PrcFail) AS Fail
	FROM (
		SELECT p.SchIdx, p.PrcDate, 
		CASE p.prcResult WHEN 1 THEN 1 END AS PrcOK,
		CASE p.prcResult WHEN 0 THEN 1 END AS PrcFail
		FROM Process AS p
	) AS Smr
GROUP BY Smr.SchIdx, Smr.PrcDate

SELECT * FROM Schedules AS sch 
	INNER JOIN Process AS prc 
	on sch.SchIdx = prc.SchIdx

SELECT sch.SchIdx, sch.PlantCode, sch.SchAmount, prc.PrcDate,
	   prc.Success, prc.Fail
	FROM Schedules AS sch 
	INNER JOIN (
		SELECT Smr.SchIdx, smr.PrcDate, SUM(Smr.PrcOK) AS Success, SUM(Smr.PrcFail) AS Fail
			FROM (
			SELECT p.SchIdx, p.PrcDate, 
			CASE p.prcResult WHEN 1 THEN 1 END AS PrcOK,
			CASE p.prcResult WHEN 0 THEN 1 END AS PrcFail
			FROM Process AS p
			) AS Smr
		GROUP BY Smr.SchIdx, Smr.PrcDate
	) AS prc
	ON sch.SchIdx = prc.SchIdx
	WHERE sch.PlantCode = 'PC010002'
	AND prc.PrcDate BETWEEN '2021-06-29' AND '2021-06-30'


	SELECT sch.SchIdx, sch.PlantCode, sch.SchAmount, prc.PrcDate,
	                                prc.PrcOkAmount, prc.PrcFailAmount
	                                FROM Schedules AS sch 
	                                INNER JOIN (
		                                SELECT Smr.SchIdx, smr.PrcDate, SUM(Smr.PrcOKAmount) AS PrcOkAmount, SUM(Smr.PrcFailAmount) AS PrcFailAmount
			                            FROM (
			                                SELECT p.SchIdx, p.PrcDate, 
			                                CASE p.prcResult WHEN 1 THEN 1 END AS PrcOKAmount,
			                                CASE p.prcResult WHEN 0 THEN 1 END AS PrcFailAmount
			                                FROM Process AS p
			                                ) AS Smr
		                                GROUP BY Smr.SchIdx, Smr.PrcDate
	                                    ) AS prc
	                                ON sch.SchIdx = prc.SchIdx
	                                WHERE sch.PlantCode = 'PC010002'
		
