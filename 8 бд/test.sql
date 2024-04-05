-- Active: 1711818553436@@127.0.0.1@5432@practice@public

SELECT 
    u.user_id, 
    u.first_name, 
    u.last_name, 
    u.login, 
    u.password, 
    c.client_id, 
    pm.manager_id,
    s.specialty_id
FROM users u
LEFT JOIN clients c ON c.user_id = u.user_id
LEFT JOIN project_managers pm ON pm.user_id = u.user_id
LEFT JOIN specialists s ON s.user_id = u.user_id
WHERE u.login = 'm1' AND u.password = '1asd'
ORDER BY u.user_id;


SELECT 
    p.project_id, 
    p.name, 
    p.cost, 
    p.start_date, 
    p.end_date, 
    p.status, 
    pm.manager_id, 
    u1.first_name,
    u1.last_name,
    c.client_id,
    u2.first_name,
    u2.last_name
FROM projects p 
JOIN project_managers pm ON p.manager_id = pm.manager_id 
JOIN users u1 ON u1.user_id = pm.user_id
JOIN clients c ON p.client_id = c.client_id
JOIN users u2 ON u2.user_id = c.user_id

SELECT     u.user_id,     u.first_name,     u.last_name,     u.login,     u.password,     c.client_id,     pm.manager_id,     s.specialty_id FROM users u LEFT JOIN clients c ON c.user_id = u.user_id LEFT JOIN project_managers pm ON pm.user_id = u.user_id LEFT JOIN specialists s ON s.user_id = u.user_id WHERE u.login = 'm2' AND u.password = '1' ORDER BY u.user_id;


 SELECT     p.project_id,      p.name,      p.cost,      p.start_date,      p.end_date,      p.status,      pm.manager_id,      u1.first_name,     u1.last_name,     c.client_id,     u2.first_name,     u2.last_name FROM projects p JOIN project_managers pm ON p.manager_id = pm.manager_id JOIN users u1 ON u1.user_id = pm.user_id JOIN clients c ON p.client_id = c.client_id JOIN users u2 ON u2.user_id = c.user_id 
 