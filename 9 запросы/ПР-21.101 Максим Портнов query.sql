-- Active: 1711818553436@@127.0.0.1@5432@practice@public
-- Выборка всех проектов с именами их менеджеров
SELECT p.name AS project_name, u.first_name, u.last_name AS manager_name
FROM projects p
JOIN project_managers pm ON p.manager_id = pm.manager_id
JOIN users u ON u.user_id = pm.user_id
ORDER BY p.name;

-- Получение списка клиентов и количества проектов для каждого
SELECT c.company_name, COUNT(p.project_id) AS project_count
FROM clients c
JOIN projects p ON c.client_id = p.client_id
GROUP BY c.company_name
ORDER BY project_count DESC;

-- Выборка проектов с превышением стоимости разработки над заданным порогом
SELECT name, cost
FROM projects
WHERE cost > 10000
ORDER BY cost DESC;

-- Поиск проектов по части названия
SELECT name
FROM projects
WHERE name LIKE '%5%'
ORDER BY name;

-- Список специалистов по специальности
SELECT u.first_name, u.last_name, sp.specialty_name
FROM specialists s
JOIN specialties sp ON s.specialty_id = sp.specialty_id
JOIN users u ON u.user_id = s.user_id
ORDER BY sp.specialty_name, u.first_name, u.last_name;

-- Общая стоимость разработки рекламных материалов по проектам
SELECT p.name, SUM(am.development_cost) AS total_development_cost
FROM advertising_materials am
JOIN projects p ON am.project_id = p.project_id
GROUP BY p.name
ORDER BY total_development_cost DESC;

-- Список задач, отсортированных по сроку выполнения и статусу
SELECT description, due_date, status
FROM tasks
ORDER BY due_date, status;

-- Количество задач по специальностям
SELECT sp.specialty_name, COUNT(t.task_id) AS task_count
FROM tasks t
JOIN specialties sp ON t.specialty_id = sp.specialty_id
GROUP BY sp.specialty_name
ORDER BY task_count DESC;

-- Список каналов распространения для определенного проекта
SELECT dc.name, dc.type, dc.audience
FROM distribution_channels dc
WHERE dc.project_id = 1
ORDER BY dc.name;

-- Анализ результатов рекламной кампании по проектам
SELECT p.name, ca.results, ca.conclusions
FROM campaign_analysis ca
JOIN projects p ON ca.project_id = p.project_id
ORDER BY p.name;

-- Список специалистов без задач
SELECT u.first_name, u.last_name
FROM specialists s
JOIN users u ON u.user_id = s.user_id
LEFT JOIN tasks t ON s.specialist_id = t.specialty_id
WHERE t.task_id IS NULL
ORDER BY u.first_name, u.last_name;

-- История изменений задачи
SELECT t.description, tch.change_date, tch.change_description
FROM task_change_history tch
JOIN tasks t ON tch.task_id = t.task_id
ORDER BY tch.change_date DESC;

-- Список проектов с их каналами распространения и стоимостью размещения
SELECT p.name, dc.name AS channel_name, dc.placement_cost
FROM projects p
JOIN distribution_channels dc ON p.project_id = dc.project_id
ORDER BY p.name, dc.placement_cost DESC;

-- Подробный отчет по рекламным кампаниям
SELECT p.name AS project_name, cr.period, cr.summary_data
FROM campaign_reports cr
JOIN projects p ON cr.project_id = p.project_id
ORDER BY cr.period DESC;

-- Список всех специализаций и количество специалистов в каждой
SELECT sp.specialty_name, COUNT(s.specialist_id) AS specialist_count
FROM specialties sp
LEFT JOIN specialists s ON sp.specialty_id = s.specialty_id
GROUP BY sp.specialty_name
ORDER BY specialist_count DESC;