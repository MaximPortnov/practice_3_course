-- Active: 1712235013311@@localhost@5432@practice@public

DROP TABLE IF EXISTS advertising_materials CASCADE;
DROP TABLE IF EXISTS campaign_analysis CASCADE;
DROP TABLE IF EXISTS campaign_reports CASCADE;
DROP TABLE IF EXISTS clients CASCADE;
DROP TABLE IF EXISTS distribution_channels CASCADE;
DROP TABLE IF EXISTS project_managers CASCADE;
DROP TABLE IF EXISTS projects CASCADE;
DROP TABLE IF EXISTS specialists CASCADE;
DROP TABLE IF EXISTS specialties CASCADE;
DROP TABLE IF EXISTS task_change_history CASCADE;
DROP TABLE IF EXISTS tasks CASCADE;
DROP TABLE IF EXISTS users CASCADE;

CREATE TABLE IF NOT EXISTS users (
    user_id SERIAL PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    login VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL
);

-- Создание таблицы менеджеров проектов
CREATE TABLE IF NOT EXISTS project_managers (
    manager_id SERIAL PRIMARY KEY,
    contact_information TEXT NOT NULL,
    specialization VARCHAR(255) NOT NULL,
    user_id INTEGER REFERENCES users(user_id) ON DELETE SET NULL
);

-- Создание таблицы клиентов
CREATE TABLE IF NOT EXISTS clients (
    client_id SERIAL PRIMARY KEY,
    company_name VARCHAR(255) NOT NULL,
    contact_information TEXT NOT NULL,
    user_id INTEGER REFERENCES users(user_id) ON DELETE SET NULL
);

-- Создание таблицы проектов
CREATE TABLE IF NOT EXISTS projects (
    project_id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    cost NUMERIC NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    status VARCHAR(255) NOT NULL,
    manager_id INTEGER REFERENCES project_managers(manager_id),
    client_id INTEGER REFERENCES clients(client_id) 
);

-- Создание таблицы рекламных материалов
CREATE TABLE IF NOT EXISTS advertising_materials (
    material_id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    type VARCHAR(255) NOT NULL,
    target_audience TEXT NOT NULL,
    development_cost NUMERIC NOT NULL,
    project_id INTEGER REFERENCES projects(project_id) ON DELETE CASCADE
);

-- Создание таблицы специальностей
CREATE TABLE IF NOT EXISTS specialties (
    specialty_id SERIAL PRIMARY KEY,
    specialty_name VARCHAR(255) NOT NULL,
    description TEXT NOT NULL
);

-- Создание таблицы задач
CREATE TABLE IF NOT EXISTS tasks (
    task_id SERIAL PRIMARY KEY,
    description TEXT NOT NULL,
    due_date DATE NOT NULL,
    status VARCHAR(255) NOT NULL,
    specialty_id INTEGER REFERENCES specialties(specialty_id) ON DELETE SET NULL,
    manager_id INTEGER REFERENCES project_managers(manager_id) ON DELETE SET NULL
);

-- Создание таблицы каналов распространения
CREATE TABLE IF NOT EXISTS distribution_channels (
    channel_id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    type VARCHAR(255) NOT NULL,
    placement_cost NUMERIC NOT NULL,
    audience TEXT NOT NULL,
    project_id INTEGER REFERENCES projects(project_id) ON DELETE SET NULL
);

-- Создание таблицы анализа кампаний
CREATE TABLE IF NOT EXISTS campaign_analysis (
    analysis_id SERIAL PRIMARY KEY,
    project_id INTEGER REFERENCES projects(project_id) ON DELETE CASCADE,
    results TEXT NOT NULL,
    conclusions TEXT NOT NULL,
    recommendations TEXT NOT NULL
);

-- Дополнительные таблицы

-- Создание таблицы специалистов
CREATE TABLE IF NOT EXISTS specialists (
    specialist_id SERIAL PRIMARY KEY,
    specialty_id INTEGER REFERENCES specialties(specialty_id) ON DELETE SET NULL,
    contact_information TEXT NOT NULL,
    user_id INTEGER REFERENCES users(user_id) ON DELETE SET NULL
);


-- Создание таблицы истории изменений задач
CREATE TABLE IF NOT EXISTS task_change_history (
    change_id SERIAL PRIMARY KEY,
    task_id INTEGER REFERENCES tasks(task_id) ON DELETE CASCADE,
    change_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    change_description TEXT NOT NULL
);

-- Создание таблицы отчетов по рекламным кампаниям
CREATE TABLE IF NOT EXISTS campaign_reports (
    report_id SERIAL PRIMARY KEY,
    project_id INTEGER REFERENCES projects(project_id) ON DELETE CASCADE,
    period TEXT NOT NULL,
    summary_data TEXT NOT NULL
);
