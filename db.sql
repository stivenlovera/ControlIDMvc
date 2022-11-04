DROP DATABASE IF EXISTS control_acceso;

CREATE database
    control_acceso CHARACTER SET 'UTF8' COLLATE 'utf8_general_ci';

use control_acceso;

DROP TABLE IF EXISTS persona;

CREATE TABLE
    persona(
        id int AUTO_INCREMENT primary key,
        ci VARCHAR(350) NOT NULL,
        nombre VARCHAR(350) NOT NULL,
        apellido VARCHAR(350) NOT NULL,
        fecha_nac DATE NOT NULL,
        email VARCHAR(350) NOT NULL,
        celular VARCHAR(350) NOT NULL,
        dirrecion VARCHAR(350) NULL,
        observaciones VARCHAR(350) NULL default '',
        usuario VARCHAR(350) NOT NULL,
        contraseña TEXT(350) NOT NULL,
        image TEXT NULL,
        image_documento TEXT NULL,
        creado_por int NULL
    );

DROP TABLE IF EXISTS persona_informacion;

CREATE TABLE
    persona_informacion(
        id int AUTO_INCREMENT primary key,
        ci VARCHAR(350) NOT NULL,
        nombre VARCHAR(350) NOT NULL,
        apellido VARCHAR(350) NOT NULL,
        fecha_nac DATE NOT NULL,
        email VARCHAR(350) NOT NULL,
        celular VARCHAR(350) NOT NULL,
        dirrecion VARCHAR(350) NOT NULL,
        observaciones VARCHAR(350) NULL default '',
        usuario VARCHAR(350) NOT NULL,
        contraseña TEXT(350) NOT NULL,
        image TEXT NULL,
        image_documento TEXT NULL,
        creado_por int NOT NULL
    );

DROP TABLE IF EXISTS persona_profesional;

CREATE TABLE
    persona_profesional(
        id int AUTO_INCREMENT primary key,
        ci VARCHAR(350) NOT NULL,
        nombre VARCHAR(350) NOT NULL,
        apellido VARCHAR(350) NOT NULL,
        fecha_nac DATE NOT NULL,
        email VARCHAR(350) NOT NULL,
        celular VARCHAR(350) NOT NULL,
        dirrecion VARCHAR(350) NOT NULL,
        observaciones VARCHAR(350) NULL default '',
        usuario VARCHAR(350) NOT NULL,
        contraseña TEXT(350) NOT NULL,
        image TEXT NULL,
        image_documento TEXT NULL,
        creado_por int NOT NULL
    );

DROP TABLE IF EXISTS persona_domiciliario;

CREATE TABLE
    persona_domiciliario(
        id int AUTO_INCREMENT primary key,
        ci VARCHAR(350) NOT NULL,
        nombre VARCHAR(350) NOT NULL,
        apellido VARCHAR(350) NOT NULL,
        fecha_nac DATE NOT NULL,
        email VARCHAR(350) NOT NULL,
        celular VARCHAR(350) NOT NULL,
        dirrecion VARCHAR(350) NOT NULL,
        observaciones VARCHAR(350) NULL default '',
        usuario VARCHAR(350) NOT NULL,
        contraseña TEXT(350) NOT NULL,
        image TEXT NULL,
        image_documento TEXT NULL,
        creado_por int NOT NULL
    );

DROP TABLE IF EXISTS persona_vehicular;

CREATE TABLE
    persona_vehicular(
        id int AUTO_INCREMENT primary key,
        ci VARCHAR(350) NOT NULL,
        nombre VARCHAR(350) NOT NULL,
        apellido VARCHAR(350) NOT NULL,
        fecha_nac DATE NOT NULL,
        email VARCHAR(350) NOT NULL,
        celular VARCHAR(350) NOT NULL,
        dirrecion VARCHAR(350) NOT NULL,
        observaciones VARCHAR(350) NULL default '',
        usuario VARCHAR(350) NOT NULL,
        contraseña TEXT(350) NOT NULL,
        image TEXT NULL,
        image_documento TEXT NULL,
        creado_por int NOT NULL
    );

DROP TABLE IF EXISTS horario;

CREATE TABLE
    horario(
        id int AUTO_INCREMENT primary key,
        nombre VARCHAR(350) NULL,
        descripcion TEXT(350) NULL
    );

DROP TABLE IF EXISTS dia;

CREATE TABLE
    dia(
        id int AUTO_INCREMENT primary key,
        nombre VARCHAR(350) NULL,
        hora_inicio DATE NULL,
        hora_fin DATE NULL,
        horario_id int NULL
    );

/*
 *ejecucion de script autorelleno
 */

INSERT INTO
    `persona`(
        `ci`,
        `nombre`,
        `apellido`,
        `fecha_nac`,
        `email`,
        `celular`,
        `dirrecion`,
        `observaciones`,
        `usuario`,
        `contraseña`,
        `image`,
        `image_documento`,
        `creado_por`
    )
VALUES (
        '8963497',
        'ali',
        'lovera',
        '1991-09-01',
        'stivenlovera@gmail',
        '75679775',
        'barrio toborochi calle los pinos',
        ' ',
        'stivenlovera',
        '12345',
        '',
        '',
        0
    );

DROP TABLE IF EXISTS persona;

CREATE TABLE
    cajaNivelUno(
        id int AUTO_INCREMENT primary key,
        codigo VARCHAR(350) NOT NULL,
        descripcion VARCHAR(350) NOT NULL,
        moneda VARCHAR(350) NOT NULL,
        nivel VARCHAR(350) NOT NULL
    );

CREATE TABLE
    cajaNivelDos(
        id int AUTO_INCREMENT primary key,
        codigo VARCHAR(350) NOT NULL,
        descripcion VARCHAR(350) NOT NULL,
        moneda VARCHAR(350) NOT NULL,
        cajaNivelUno int NOT NULL, 
        FOREIGN KEY (cajaNivelUno) REFERENCES cajaNivelUno(id)
    );

CREATE TABLE
    cajaNivelTres(
        id int AUTO_INCREMENT primary key,
        codigo VARCHAR(350) NOT NULL,
        descripcion VARCHAR(350) NOT NULL,
        moneda VARCHAR(350) NOT NULL,
        cajaNivelDos int NOT NULL, 
        FOREIGN KEY (cajaNivelDos) REFERENCES cajaNivelDos(id)
    );

CREATE TABLE
    cajaNivelCuatro(
        id int AUTO_INCREMENT primary key,
        codigo VARCHAR(350) NOT NULL,
        descripcion VARCHAR(350) NOT NULL,
        moneda VARCHAR(350) NOT NULL,
        cajaNivelTres int NOT NULL,
        FOREIGN KEY (cajaNivelTres) REFERENCES cajaNivelTres(id)
    );