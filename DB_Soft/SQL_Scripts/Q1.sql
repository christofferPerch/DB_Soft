SELECT 
    o.OrganizationName AS Organization, 
    MAX(CAST(te.PercentageReductionTarget AS FLOAT)) AS MaxPercentageReductionTarget,
    MAX(te.TargetDate) AS LatestTargetDate 
FROM 
    [dbo].[Organization] o
JOIN 
    [dbo].[TargetEmission] te ON o.AccountNo = te.AccountNo
GROUP BY 
    o.OrganizationName
ORDER BY 
    MaxPercentageReductionTarget DESC, 
    LatestTargetDate
