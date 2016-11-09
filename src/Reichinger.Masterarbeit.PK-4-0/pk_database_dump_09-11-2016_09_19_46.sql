--
-- PostgreSQL database cluster dump
--

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Drop databases
--

DROP DATABASE "pk-database";




--
-- Drop roles
--

DROP ROLE dbadmin;
DROP ROLE postgres;


--
-- Roles
--

CREATE ROLE dbadmin;
ALTER ROLE dbadmin WITH SUPERUSER INHERIT NOCREATEROLE NOCREATEDB LOGIN NOREPLICATION PASSWORD 'md588bcfabd2fda7bfdcf73c2b21fe580eb';
CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION;






--
-- Database creation
--

CREATE DATABASE "pk-database" WITH TEMPLATE = template0 OWNER = dbadmin;
REVOKE ALL ON DATABASE template1 FROM PUBLIC;
REVOKE ALL ON DATABASE template1 FROM postgres;
GRANT ALL ON DATABASE template1 TO postgres;
GRANT CONNECT ON DATABASE template1 TO PUBLIC;


\connect -reuse-previous=on "dbname='pk-database'"

SET default_transaction_read_only = off;

--
-- PostgreSQL database dump
--

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: app_user; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE app_user (
    id integer NOT NULL,
    firstname character varying(50) NOT NULL,
    lastname character varying(50) NOT NULL,
    email character varying(50) NOT NULL,
    password character(128) NOT NULL,
    salt_string character varying(50) NOT NULL,
    mat_nr integer NOT NULL,
    ldap_id integer NOT NULL,
    active boolean,
    created timestamp without time zone DEFAULT ('now'::text)::date NOT NULL
);


ALTER TABLE public.app_user OWNER TO dbadmin;

--
-- Name: application; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE application (
    id integer NOT NULL,
    created timestamp without time zone DEFAULT ('now'::text)::date NOT NULL,
    last_modified timestamp without time zone NOT NULL,
    filled_form json,
    version integer NOT NULL,
    is_current boolean NOT NULL,
    previous_version integer NOT NULL,
    user_id integer NOT NULL,
    conference_id integer,
    status_id integer NOT NULL,
    form_id integer NOT NULL
);


ALTER TABLE public.application OWNER TO dbadmin;

--
-- Name: asignee; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE asignee (
    id integer NOT NULL,
    application_id integer NOT NULL,
    user_id integer NOT NULL
);


ALTER TABLE public.asignee OWNER TO dbadmin;

--
-- Name: comment; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE comment (
    id integer NOT NULL,
    text text NOT NULL,
    created timestamp without time zone DEFAULT ('now'::text)::date NOT NULL,
    is_private boolean DEFAULT false NOT NULL,
    user_id integer NOT NULL,
    application_id integer NOT NULL
);


ALTER TABLE public.comment OWNER TO dbadmin;

--
-- Name: conference; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE conference (
    id integer NOT NULL,
    description text,
    date_of_event timestamp without time zone NOT NULL
);


ALTER TABLE public.conference OWNER TO dbadmin;

--
-- Name: field_type; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE field_type (
    id integer NOT NULL,
    description character varying(50) NOT NULL
);


ALTER TABLE public.field_type OWNER TO dbadmin;

--
-- Name: form; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE form (
    id integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.form OWNER TO dbadmin;

--
-- Name: form_field; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE form_field (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    field_type integer NOT NULL
);


ALTER TABLE public.form_field OWNER TO dbadmin;

--
-- Name: form_has_field; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE form_has_field (
    id integer NOT NULL,
    required boolean DEFAULT false NOT NULL,
    label character varying(50) NOT NULL,
    position_index integer NOT NULL,
    styling text NOT NULL,
    form_id integer NOT NULL,
    form_field_id integer NOT NULL
);


ALTER TABLE public.form_has_field OWNER TO dbadmin;

--
-- Name: permission; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE permission (
    id integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.permission OWNER TO dbadmin;

--
-- Name: role; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE role (
    id integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.role OWNER TO dbadmin;

--
-- Name: role_permission; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE role_permission (
    id integer NOT NULL,
    role_id integer NOT NULL,
    permission_id integer NOT NULL
);


ALTER TABLE public.role_permission OWNER TO dbadmin;

--
-- Name: status; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE status (
    id integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.status OWNER TO dbadmin;

--
-- Name: user_has_role; Type: TABLE; Schema: public; Owner: dbadmin; Tablespace: 
--

CREATE TABLE user_has_role (
    id integer NOT NULL,
    role_id integer NOT NULL,
    user_id integer NOT NULL
);


ALTER TABLE public.user_has_role OWNER TO dbadmin;

--
-- Data for Name: app_user; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY app_user (id, firstname, lastname, email, password, salt_string, mat_nr, ldap_id, active, created) FROM stdin;
\.


--
-- Data for Name: application; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY application (id, created, last_modified, filled_form, version, is_current, previous_version, user_id, conference_id, status_id, form_id) FROM stdin;
\.


--
-- Data for Name: asignee; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY asignee (id, application_id, user_id) FROM stdin;
\.


--
-- Data for Name: comment; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY comment (id, text, created, is_private, user_id, application_id) FROM stdin;
\.


--
-- Data for Name: conference; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY conference (id, description, date_of_event) FROM stdin;
\.


--
-- Data for Name: field_type; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY field_type (id, description) FROM stdin;
\.


--
-- Data for Name: form; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY form (id, name) FROM stdin;
\.


--
-- Data for Name: form_field; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY form_field (id, name, field_type) FROM stdin;
\.


--
-- Data for Name: form_has_field; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY form_has_field (id, required, label, position_index, styling, form_id, form_field_id) FROM stdin;
\.


--
-- Data for Name: permission; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY permission (id, name) FROM stdin;
\.


--
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY role (id, name) FROM stdin;
\.


--
-- Data for Name: role_permission; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY role_permission (id, role_id, permission_id) FROM stdin;
\.


--
-- Data for Name: status; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY status (id, name) FROM stdin;
\.


--
-- Data for Name: user_has_role; Type: TABLE DATA; Schema: public; Owner: dbadmin
--

COPY user_has_role (id, role_id, user_id) FROM stdin;
\.


--
-- Name: app_user_mat_nr_key; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY app_user
    ADD CONSTRAINT app_user_mat_nr_key UNIQUE (mat_nr);


--
-- Name: app_user_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY app_user
    ADD CONSTRAINT app_user_pkey PRIMARY KEY (id);


--
-- Name: application_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY application
    ADD CONSTRAINT application_pkey PRIMARY KEY (id);


--
-- Name: asignee_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY asignee
    ADD CONSTRAINT asignee_pkey PRIMARY KEY (id);


--
-- Name: comment_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY comment
    ADD CONSTRAINT comment_pkey PRIMARY KEY (id);


--
-- Name: conference_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY conference
    ADD CONSTRAINT conference_pkey PRIMARY KEY (id);


--
-- Name: field_type_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY field_type
    ADD CONSTRAINT field_type_pkey PRIMARY KEY (id);


--
-- Name: form_field_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY form_field
    ADD CONSTRAINT form_field_pkey PRIMARY KEY (id);


--
-- Name: form_has_field_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY form_has_field
    ADD CONSTRAINT form_has_field_pkey PRIMARY KEY (id);


--
-- Name: form_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY form
    ADD CONSTRAINT form_pkey PRIMARY KEY (id);


--
-- Name: permission_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY permission
    ADD CONSTRAINT permission_pkey PRIMARY KEY (id);


--
-- Name: role_permission_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY role_permission
    ADD CONSTRAINT role_permission_pkey PRIMARY KEY (id);


--
-- Name: role_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY role
    ADD CONSTRAINT role_pkey PRIMARY KEY (id);


--
-- Name: status_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY status
    ADD CONSTRAINT status_pkey PRIMARY KEY (id);


--
-- Name: user_has_role_pkey; Type: CONSTRAINT; Schema: public; Owner: dbadmin; Tablespace: 
--

ALTER TABLE ONLY user_has_role
    ADD CONSTRAINT user_has_role_pkey PRIMARY KEY (id);


--
-- Name: application_conference_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY application
    ADD CONSTRAINT application_conference_id_fkey FOREIGN KEY (conference_id) REFERENCES conference(id);


--
-- Name: application_form_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY application
    ADD CONSTRAINT application_form_id_fkey FOREIGN KEY (form_id) REFERENCES form(id);


--
-- Name: application_previous_version_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY application
    ADD CONSTRAINT application_previous_version_fkey FOREIGN KEY (previous_version) REFERENCES application(id);


--
-- Name: application_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY application
    ADD CONSTRAINT application_status_id_fkey FOREIGN KEY (status_id) REFERENCES status(id);


--
-- Name: application_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY application
    ADD CONSTRAINT application_user_id_fkey FOREIGN KEY (user_id) REFERENCES app_user(id);


--
-- Name: asignee_application_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY asignee
    ADD CONSTRAINT asignee_application_id_fkey FOREIGN KEY (application_id) REFERENCES application(id);


--
-- Name: asignee_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY asignee
    ADD CONSTRAINT asignee_user_id_fkey FOREIGN KEY (user_id) REFERENCES app_user(id);


--
-- Name: comment_application_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY comment
    ADD CONSTRAINT comment_application_id_fkey FOREIGN KEY (application_id) REFERENCES application(id);


--
-- Name: comment_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY comment
    ADD CONSTRAINT comment_user_id_fkey FOREIGN KEY (user_id) REFERENCES app_user(id);


--
-- Name: form_field_field_type_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY form_field
    ADD CONSTRAINT form_field_field_type_fkey FOREIGN KEY (field_type) REFERENCES field_type(id);


--
-- Name: form_has_field_form_field_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY form_has_field
    ADD CONSTRAINT form_has_field_form_field_id_fkey FOREIGN KEY (form_field_id) REFERENCES form_field(id);


--
-- Name: form_has_field_form_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY form_has_field
    ADD CONSTRAINT form_has_field_form_id_fkey FOREIGN KEY (form_id) REFERENCES form(id);


--
-- Name: role_permission_permission_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY role_permission
    ADD CONSTRAINT role_permission_permission_id_fkey FOREIGN KEY (permission_id) REFERENCES permission(id);


--
-- Name: role_permission_role_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY role_permission
    ADD CONSTRAINT role_permission_role_id_fkey FOREIGN KEY (role_id) REFERENCES role(id);


--
-- Name: user_has_role_role_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY user_has_role
    ADD CONSTRAINT user_has_role_role_id_fkey FOREIGN KEY (role_id) REFERENCES role(id);


--
-- Name: user_has_role_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: dbadmin
--

ALTER TABLE ONLY user_has_role
    ADD CONSTRAINT user_has_role_user_id_fkey FOREIGN KEY (user_id) REFERENCES app_user(id);


--
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

\connect postgres

SET default_transaction_read_only = off;

--
-- PostgreSQL database dump
--

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- Name: postgres; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE postgres IS 'default administrative connection database';


--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

\connect template1

SET default_transaction_read_only = off;

--
-- PostgreSQL database dump
--

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- Name: template1; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE template1 IS 'default template for new databases';


--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

--
-- PostgreSQL database cluster dump complete
--

