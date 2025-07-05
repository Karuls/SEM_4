USE UNIVER;

-- Проверка средней оценки
IF (SELECT AVG(NOTE) FROM PROGRESS) < 7
    PRINT 'Средняя оценка ниже 7';
ELSE
    PRINT 'Средняя оценка 7 или выше';

-- Проверка количества записей
IF (SELECT COUNT(*) FROM PROGRESS) > 100
    PRINT 'Записей много (>100)';
ELSE
    PRINT 'Записей мало (≤100)';
