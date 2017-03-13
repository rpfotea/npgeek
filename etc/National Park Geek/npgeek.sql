select * from weather

SELECT * FROM weather WHERE parkCode ='ENP'

select * from survey_result

SELECT park.parkName, park.parkCode, COUNT(survey_result.parkCode) AS Total_Survey_Park
FROM survey_result
INNER JOIN park ON survey_result.parkCode=park.parkCode
GROUP BY park.parkName, park.parkCode
ORDER BY Total_Survey_Park DESC