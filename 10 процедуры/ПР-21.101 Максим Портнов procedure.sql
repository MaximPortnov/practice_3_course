-- Active: 1711818553436@@127.0.0.1@5432@practice@public
-- Создание процедуры регистрации клиента
CREATE OR REPLACE PROCEDURE register_client(
    p_company_name VARCHAR,
    p_contact_information TEXT)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO clients (company_name, contact_information) 
    VALUES (p_company_name, p_contact_information);
END;
$$;

-- Создание процедуры обновления контактной информации клиента
CREATE OR REPLACE PROCEDURE update_client_contact(
    p_client_id INTEGER,
    p_new_contact_information TEXT)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE clients
    SET contact_information = p_new_contact_information
    WHERE client_id = p_client_id;
END;
$$;

-- Создание процедуры для обновления статуса всех задач, привязанных к проекту
CREATE OR REPLACE PROCEDURE update_task_status_by_project(t_task_id INTEGER, p_new_status VARCHAR)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE tasks
    SET status = p_new_status
    WHERE task_id = t_task_id;
END;
$$;


CALL register_client('Example Company', 'contact@example.com');

CALL update_client_contact(1, 'newcontact@example.com');

CALL update_task_status_by_project(1, 'Completed');
