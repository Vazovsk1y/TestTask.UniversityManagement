# Test Assignment - ASP.NET Core Web API

## Overview
You need to create an ASP.NET Core Web API that will provide CRUD (Create, Read, Update, Delete) operations for university related entities. The API should include the following set of entities: Department, Group, Student, EducationContract, Speciality (the set of properties for the entities is at your discretion).

## Functionality

- Retrieve student information by Id.
- Add a new speciality.
- Retrieve a list of all departments (with pagination).
- Retrieve a list of all specialities.
- Update student information by Id.
- Delete (expel) a student by Id.
- Renew a contract (update).

## Remarks

- Implement the API using minimal-API.
- Use the micro-ORM Dapper for data access.
- Use FluentMigrator for creating migrations.