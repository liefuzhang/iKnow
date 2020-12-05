--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1
-- Dumped by pg_dump version 13.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Activities; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Activities" (
    "Id" integer NOT NULL,
    "AppUserId" character varying(128),
    "Type" integer NOT NULL,
    "TopicId" integer NOT NULL,
    "QuestionId" integer NOT NULL,
    "AnswerId" integer NOT NULL,
    "DateTime" timestamp(3) without time zone NOT NULL,
    "TRIAL297" character(1)
);


ALTER TABLE public."Activities" OWNER TO postgres;

--
-- Name: TABLE "Activities"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."Activities" IS 'TRIAL';


--
-- Name: COLUMN "Activities"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Activities"."Id" IS 'TRIAL';


--
-- Name: COLUMN "Activities"."AppUserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Activities"."AppUserId" IS 'TRIAL';


--
-- Name: COLUMN "Activities"."Type"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Activities"."Type" IS 'TRIAL';


--
-- Name: COLUMN "Activities"."TopicId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Activities"."TopicId" IS 'TRIAL';


--
-- Name: COLUMN "Activities"."QuestionId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Activities"."QuestionId" IS 'TRIAL';


--
-- Name: COLUMN "Activities"."AnswerId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Activities"."AnswerId" IS 'TRIAL';


--
-- Name: COLUMN "Activities"."DateTime"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Activities"."DateTime" IS 'TRIAL';


--
-- Name: COLUMN "Activities"."TRIAL297"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Activities"."TRIAL297" IS 'TRIAL';


--
-- Name: Activities_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Activities_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Activities_Id_seq" OWNER TO postgres;

--
-- Name: Activities_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Activities_Id_seq" OWNED BY public."Activities"."Id";


--
-- Name: AnswerLikes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AnswerLikes" (
    "Id" integer NOT NULL,
    "AppUserId" character varying(128),
    "AnswerId" integer NOT NULL,
    "TRIAL301" character(1)
);


ALTER TABLE public."AnswerLikes" OWNER TO postgres;

--
-- Name: TABLE "AnswerLikes"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."AnswerLikes" IS 'TRIAL';


--
-- Name: COLUMN "AnswerLikes"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AnswerLikes"."Id" IS 'TRIAL';


--
-- Name: COLUMN "AnswerLikes"."AppUserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AnswerLikes"."AppUserId" IS 'TRIAL';


--
-- Name: COLUMN "AnswerLikes"."AnswerId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AnswerLikes"."AnswerId" IS 'TRIAL';


--
-- Name: COLUMN "AnswerLikes"."TRIAL301"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AnswerLikes"."TRIAL301" IS 'TRIAL';


--
-- Name: AnswerLikes_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."AnswerLikes_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."AnswerLikes_Id_seq" OWNER TO postgres;

--
-- Name: AnswerLikes_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."AnswerLikes_Id_seq" OWNED BY public."AnswerLikes"."Id";


--
-- Name: Answers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Answers" (
    "Id" integer NOT NULL,
    "Content" text NOT NULL,
    "CreatedDate" timestamp(3) without time zone NOT NULL,
    "QuestionId" integer NOT NULL,
    "AppUserId" character varying(128) NOT NULL,
    "UpdatedDate" timestamp(3) without time zone,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "TRIAL304" character(1)
);


ALTER TABLE public."Answers" OWNER TO postgres;

--
-- Name: TABLE "Answers"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."Answers" IS 'TRIAL';


--
-- Name: COLUMN "Answers"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Answers"."Id" IS 'TRIAL';


--
-- Name: COLUMN "Answers"."Content"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Answers"."Content" IS 'TRIAL';


--
-- Name: COLUMN "Answers"."CreatedDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Answers"."CreatedDate" IS 'TRIAL';


--
-- Name: COLUMN "Answers"."QuestionId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Answers"."QuestionId" IS 'TRIAL';


--
-- Name: COLUMN "Answers"."AppUserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Answers"."AppUserId" IS 'TRIAL';


--
-- Name: COLUMN "Answers"."UpdatedDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Answers"."UpdatedDate" IS 'TRIAL';


--
-- Name: COLUMN "Answers"."IsDeleted"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Answers"."IsDeleted" IS 'TRIAL';


--
-- Name: COLUMN "Answers"."TRIAL304"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Answers"."TRIAL304" IS 'TRIAL';


--
-- Name: Answers_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Answers_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Answers_Id_seq" OWNER TO postgres;

--
-- Name: Answers_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Answers_Id_seq" OWNED BY public."Answers"."Id";


--
-- Name: AspNetRoleClaims; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetRoleClaims" (
    "Id" integer NOT NULL,
    "ClaimType" text,
    "ClaimValue" text,
    "RoleId" character varying(128) NOT NULL,
    "TRIAL307" character(1)
);


ALTER TABLE public."AspNetRoleClaims" OWNER TO postgres;

--
-- Name: TABLE "AspNetRoleClaims"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."AspNetRoleClaims" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoleClaims"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoleClaims"."Id" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoleClaims"."ClaimType"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoleClaims"."ClaimType" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoleClaims"."ClaimValue"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoleClaims"."ClaimValue" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoleClaims"."RoleId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoleClaims"."RoleId" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoleClaims"."TRIAL307"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoleClaims"."TRIAL307" IS 'TRIAL';


--
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."AspNetRoleClaims_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."AspNetRoleClaims_Id_seq" OWNER TO postgres;

--
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."AspNetRoleClaims_Id_seq" OWNED BY public."AspNetRoleClaims"."Id";


--
-- Name: AspNetRoles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetRoles" (
    "Id" character varying(128) NOT NULL,
    "Name" character varying(256) NOT NULL,
    "NormalizedName" character varying(256),
    "ConcurrencyStamp" text,
    "TRIAL311" character(1)
);


ALTER TABLE public."AspNetRoles" OWNER TO postgres;

--
-- Name: TABLE "AspNetRoles"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."AspNetRoles" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoles"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoles"."Id" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoles"."Name"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoles"."Name" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoles"."NormalizedName"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoles"."NormalizedName" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoles"."ConcurrencyStamp"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoles"."ConcurrencyStamp" IS 'TRIAL';


--
-- Name: COLUMN "AspNetRoles"."TRIAL311"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetRoles"."TRIAL311" IS 'TRIAL';


--
-- Name: AspNetUserClaims; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUserClaims" (
    "Id" integer NOT NULL,
    "UserId" character varying(128) NOT NULL,
    "ClaimType" text,
    "ClaimValue" text,
    "TRIAL314" character(1)
);


ALTER TABLE public."AspNetUserClaims" OWNER TO postgres;

--
-- Name: TABLE "AspNetUserClaims"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."AspNetUserClaims" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserClaims"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserClaims"."Id" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserClaims"."UserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserClaims"."UserId" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserClaims"."ClaimType"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserClaims"."ClaimType" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserClaims"."ClaimValue"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserClaims"."ClaimValue" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserClaims"."TRIAL314"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserClaims"."TRIAL314" IS 'TRIAL';


--
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."AspNetUserClaims_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."AspNetUserClaims_Id_seq" OWNER TO postgres;

--
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."AspNetUserClaims_Id_seq" OWNED BY public."AspNetUserClaims"."Id";


--
-- Name: AspNetUserLogins; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUserLogins" (
    "LoginProvider" character varying(128) NOT NULL,
    "ProviderKey" character varying(128) NOT NULL,
    "UserId" character varying(128) NOT NULL,
    "ProviderDisplayName" character varying(255),
    "TRIAL317" character(1)
);


ALTER TABLE public."AspNetUserLogins" OWNER TO postgres;

--
-- Name: TABLE "AspNetUserLogins"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."AspNetUserLogins" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserLogins"."LoginProvider"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserLogins"."LoginProvider" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserLogins"."ProviderKey"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserLogins"."ProviderKey" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserLogins"."UserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserLogins"."UserId" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserLogins"."ProviderDisplayName"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserLogins"."ProviderDisplayName" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserLogins"."TRIAL317"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserLogins"."TRIAL317" IS 'TRIAL';


--
-- Name: AspNetUserRoles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUserRoles" (
    "UserId" character varying(128) NOT NULL,
    "RoleId" character varying(128) NOT NULL,
    "TRIAL320" character(1)
);


ALTER TABLE public."AspNetUserRoles" OWNER TO postgres;

--
-- Name: TABLE "AspNetUserRoles"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."AspNetUserRoles" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserRoles"."UserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserRoles"."UserId" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserRoles"."RoleId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserRoles"."RoleId" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserRoles"."TRIAL320"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserRoles"."TRIAL320" IS 'TRIAL';


--
-- Name: AspNetUserTokens; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUserTokens" (
    "UserId" character varying(450) NOT NULL,
    "LoginProvider" character varying(450) NOT NULL,
    "Name" character varying(450) NOT NULL,
    "Value" text,
    "TRIAL327" character(1)
);


ALTER TABLE public."AspNetUserTokens" OWNER TO postgres;

--
-- Name: TABLE "AspNetUserTokens"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."AspNetUserTokens" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserTokens"."UserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserTokens"."UserId" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserTokens"."LoginProvider"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserTokens"."LoginProvider" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserTokens"."Name"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserTokens"."Name" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserTokens"."Value"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserTokens"."Value" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUserTokens"."TRIAL327"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUserTokens"."TRIAL327" IS 'TRIAL';


--
-- Name: AspNetUsers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUsers" (
    "Email" character varying(256),
    "EmailConfirmed" boolean DEFAULT false NOT NULL,
    "PasswordHash" text,
    "SecurityStamp" text,
    "PhoneNumber" text,
    "PhoneNumberConfirmed" boolean DEFAULT false NOT NULL,
    "TwoFactorEnabled" boolean DEFAULT false NOT NULL,
    "LockoutEndDateUtc" timestamp(3) without time zone,
    "LockoutEnabled" boolean DEFAULT false NOT NULL,
    "AccessFailedCount" integer DEFAULT 0 NOT NULL,
    "UserName" character varying(256) DEFAULT ''::character varying NOT NULL,
    "Id" character varying(128) DEFAULT ''::character varying NOT NULL,
    "FirstName" character varying(50) DEFAULT ''::character varying NOT NULL,
    "LastName" character varying(50) DEFAULT ''::character varying NOT NULL,
    "Intro" character varying(255),
    "Gender" smallint DEFAULT 0 NOT NULL,
    "Location" character varying(50),
    "DefaultIconNumber" smallint DEFAULT 0 NOT NULL,
    "ConcurrencyStamp" character varying(255),
    "LockoutEnd" timestamp(3) without time zone,
    "NormalizedEmail" character varying(255),
    "NormalizedUserName" character varying(255),
    "TRIAL324" character(1)
);


ALTER TABLE public."AspNetUsers" OWNER TO postgres;

--
-- Name: TABLE "AspNetUsers"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."AspNetUsers" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."Email"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."Email" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."EmailConfirmed"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."EmailConfirmed" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."PasswordHash"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."PasswordHash" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."SecurityStamp"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."SecurityStamp" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."PhoneNumber"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."PhoneNumber" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."PhoneNumberConfirmed"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."PhoneNumberConfirmed" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."TwoFactorEnabled"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."TwoFactorEnabled" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."LockoutEndDateUtc"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."LockoutEndDateUtc" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."LockoutEnabled"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."LockoutEnabled" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."AccessFailedCount"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."AccessFailedCount" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."UserName"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."UserName" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."Id" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."FirstName"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."FirstName" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."LastName"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."LastName" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."Intro"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."Intro" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."Gender"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."Gender" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."Location"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."Location" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."DefaultIconNumber"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."DefaultIconNumber" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."ConcurrencyStamp"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."ConcurrencyStamp" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."LockoutEnd"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."LockoutEnd" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."NormalizedEmail"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."NormalizedEmail" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."NormalizedUserName"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."NormalizedUserName" IS 'TRIAL';


--
-- Name: COLUMN "AspNetUsers"."TRIAL324"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."AspNetUsers"."TRIAL324" IS 'TRIAL';


--
-- Name: Comments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Comments" (
    "Id" integer NOT NULL,
    "Content" text NOT NULL,
    "CreatedDate" timestamp(3) without time zone NOT NULL,
    "AnswerId" integer NOT NULL,
    "AppUserId" character varying(128),
    "ReplyToCommentId" integer,
    "TRIAL327" character(1)
);


ALTER TABLE public."Comments" OWNER TO postgres;

--
-- Name: TABLE "Comments"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."Comments" IS 'TRIAL';


--
-- Name: COLUMN "Comments"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Comments"."Id" IS 'TRIAL';


--
-- Name: COLUMN "Comments"."Content"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Comments"."Content" IS 'TRIAL';


--
-- Name: COLUMN "Comments"."CreatedDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Comments"."CreatedDate" IS 'TRIAL';


--
-- Name: COLUMN "Comments"."AnswerId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Comments"."AnswerId" IS 'TRIAL';


--
-- Name: COLUMN "Comments"."AppUserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Comments"."AppUserId" IS 'TRIAL';


--
-- Name: COLUMN "Comments"."ReplyToCommentId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Comments"."ReplyToCommentId" IS 'TRIAL';


--
-- Name: COLUMN "Comments"."TRIAL327"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Comments"."TRIAL327" IS 'TRIAL';


--
-- Name: Comments_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Comments_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Comments_Id_seq" OWNER TO postgres;

--
-- Name: Comments_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Comments_Id_seq" OWNED BY public."Comments"."Id";


--
-- Name: Questions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Questions" (
    "Id" integer NOT NULL,
    "Title" character varying(255) NOT NULL,
    "Description" character varying(1000),
    "AppUserId" character varying(128) NOT NULL,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "TRIAL330" character(1)
);


ALTER TABLE public."Questions" OWNER TO postgres;

--
-- Name: TABLE "Questions"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."Questions" IS 'TRIAL';


--
-- Name: COLUMN "Questions"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Questions"."Id" IS 'TRIAL';


--
-- Name: COLUMN "Questions"."Title"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Questions"."Title" IS 'TRIAL';


--
-- Name: COLUMN "Questions"."Description"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Questions"."Description" IS 'TRIAL';


--
-- Name: COLUMN "Questions"."AppUserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Questions"."AppUserId" IS 'TRIAL';


--
-- Name: COLUMN "Questions"."IsDeleted"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Questions"."IsDeleted" IS 'TRIAL';


--
-- Name: COLUMN "Questions"."TRIAL330"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Questions"."TRIAL330" IS 'TRIAL';


--
-- Name: Questions_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Questions_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Questions_Id_seq" OWNER TO postgres;

--
-- Name: Questions_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Questions_Id_seq" OWNED BY public."Questions"."Id";


--
-- Name: TopicFollowings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TopicFollowings" (
    "UserId" character varying(128) NOT NULL,
    "TopicId" integer NOT NULL,
    "TRIAL333" character(1)
);


ALTER TABLE public."TopicFollowings" OWNER TO postgres;

--
-- Name: TABLE "TopicFollowings"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."TopicFollowings" IS 'TRIAL';


--
-- Name: COLUMN "TopicFollowings"."UserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."TopicFollowings"."UserId" IS 'TRIAL';


--
-- Name: COLUMN "TopicFollowings"."TopicId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."TopicFollowings"."TopicId" IS 'TRIAL';


--
-- Name: COLUMN "TopicFollowings"."TRIAL333"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."TopicFollowings"."TRIAL333" IS 'TRIAL';


--
-- Name: TopicQuestions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TopicQuestions" (
    "QuestionId" integer NOT NULL,
    "TopicId" integer NOT NULL,
    "TRIAL337" character(1)
);


ALTER TABLE public."TopicQuestions" OWNER TO postgres;

--
-- Name: TABLE "TopicQuestions"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."TopicQuestions" IS 'TRIAL';


--
-- Name: COLUMN "TopicQuestions"."QuestionId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."TopicQuestions"."QuestionId" IS 'TRIAL';


--
-- Name: COLUMN "TopicQuestions"."TopicId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."TopicQuestions"."TopicId" IS 'TRIAL';


--
-- Name: COLUMN "TopicQuestions"."TRIAL337"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."TopicQuestions"."TRIAL337" IS 'TRIAL';


--
-- Name: TopicUsers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TopicUsers" (
    "UserId" character varying(128) NOT NULL,
    "TopicId" integer NOT NULL,
    "TRIAL340" character(1)
);


ALTER TABLE public."TopicUsers" OWNER TO postgres;

--
-- Name: TABLE "TopicUsers"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."TopicUsers" IS 'TRIAL';


--
-- Name: COLUMN "TopicUsers"."UserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."TopicUsers"."UserId" IS 'TRIAL';


--
-- Name: COLUMN "TopicUsers"."TopicId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."TopicUsers"."TopicId" IS 'TRIAL';


--
-- Name: COLUMN "TopicUsers"."TRIAL340"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."TopicUsers"."TRIAL340" IS 'TRIAL';


--
-- Name: Topics; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Topics" (
    "Id" integer NOT NULL,
    "Name" character varying(50) NOT NULL,
    "Description" character varying(1000),
    "TRIAL340" character(1)
);


ALTER TABLE public."Topics" OWNER TO postgres;

--
-- Name: TABLE "Topics"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."Topics" IS 'TRIAL';


--
-- Name: COLUMN "Topics"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Topics"."Id" IS 'TRIAL';


--
-- Name: COLUMN "Topics"."Name"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Topics"."Name" IS 'TRIAL';


--
-- Name: COLUMN "Topics"."Description"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Topics"."Description" IS 'TRIAL';


--
-- Name: COLUMN "Topics"."TRIAL340"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."Topics"."TRIAL340" IS 'TRIAL';


--
-- Name: Topics_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Topics_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Topics_Id_seq" OWNER TO postgres;

--
-- Name: Topics_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Topics_Id_seq" OWNED BY public."Topics"."Id";


--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    "TRIAL291" character(1)
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Name: TABLE "__EFMigrationsHistory"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."__EFMigrationsHistory" IS 'TRIAL';


--
-- Name: COLUMN "__EFMigrationsHistory"."MigrationId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."__EFMigrationsHistory"."MigrationId" IS 'TRIAL';


--
-- Name: COLUMN "__EFMigrationsHistory"."ProductVersion"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."__EFMigrationsHistory"."ProductVersion" IS 'TRIAL';


--
-- Name: COLUMN "__EFMigrationsHistory"."TRIAL291"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."__EFMigrationsHistory"."TRIAL291" IS 'TRIAL';


--
-- Name: __MigrationHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__MigrationHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ContextKey" character varying(300) NOT NULL,
    "Model" bytea NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    "TRIAL294" character(1)
);


ALTER TABLE public."__MigrationHistory" OWNER TO postgres;

--
-- Name: TABLE "__MigrationHistory"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public."__MigrationHistory" IS 'TRIAL';


--
-- Name: COLUMN "__MigrationHistory"."MigrationId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."__MigrationHistory"."MigrationId" IS 'TRIAL';


--
-- Name: COLUMN "__MigrationHistory"."ContextKey"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."__MigrationHistory"."ContextKey" IS 'TRIAL';


--
-- Name: COLUMN "__MigrationHistory"."Model"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."__MigrationHistory"."Model" IS 'TRIAL';


--
-- Name: COLUMN "__MigrationHistory"."ProductVersion"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."__MigrationHistory"."ProductVersion" IS 'TRIAL';


--
-- Name: COLUMN "__MigrationHistory"."TRIAL294"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public."__MigrationHistory"."TRIAL294" IS 'TRIAL';


--
-- Name: Activities Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Activities" ALTER COLUMN "Id" SET DEFAULT nextval('public."Activities_Id_seq"'::regclass);


--
-- Name: AnswerLikes Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnswerLikes" ALTER COLUMN "Id" SET DEFAULT nextval('public."AnswerLikes_Id_seq"'::regclass);


--
-- Name: Answers Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Answers" ALTER COLUMN "Id" SET DEFAULT nextval('public."Answers_Id_seq"'::regclass);


--
-- Name: AspNetRoleClaims Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetRoleClaims" ALTER COLUMN "Id" SET DEFAULT nextval('public."AspNetRoleClaims_Id_seq"'::regclass);


--
-- Name: AspNetUserClaims Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserClaims" ALTER COLUMN "Id" SET DEFAULT nextval('public."AspNetUserClaims_Id_seq"'::regclass);


--
-- Name: Comments Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Comments" ALTER COLUMN "Id" SET DEFAULT nextval('public."Comments_Id_seq"'::regclass);


--
-- Name: Questions Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Questions" ALTER COLUMN "Id" SET DEFAULT nextval('public."Questions_Id_seq"'::regclass);


--
-- Name: Topics Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Topics" ALTER COLUMN "Id" SET DEFAULT nextval('public."Topics_Id_seq"'::regclass);


--
-- Data for Name: Activities; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Activities" ("Id", "AppUserId", "Type", "TopicId", "QuestionId", "AnswerId", "DateTime", "TRIAL297") FROM stdin;
1	fe8c5539-5945-4cfb-9892-6cb4d3935c90	3	0	18	0	2017-05-19 19:29:00	T
2	fe8c5539-5945-4cfb-9892-6cb4d3935c90	3	0	19	0	2017-11-11 19:29:00	T
3	aa32eb82-d698-468e-8c0b-dc3a6bebfc12	3	0	21	0	2017-08-05 19:29:00	T
4	fe8c5539-5945-4cfb-9892-6cb4d3935c90	3	0	22	0	2017-11-15 19:29:00	T
5	5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9	3	0	36	0	2017-11-25 19:29:00	T
6	5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9	3	0	37	0	2017-11-05 19:29:00	T
7	4f3518e8-abf7-457f-b6ab-15e37e99b81b	3	0	38	0	2017-11-11 19:29:00	T
8	4f3518e8-abf7-457f-b6ab-15e37e99b81b	3	0	39	0	2017-11-12 19:29:00	T
9	75245e73-98c1-4828-a7b7-77c0eefa3d7d	3	0	40	0	2017-11-18 19:29:00	T
10	cef53707-04ea-45d5-b7ac-c86f87aa625d	3	0	41	0	2017-12-15 19:29:00	T
11	d29c1e65-8eff-4459-bbce-a5f39714e011	3	0	42	0	2017-10-15 19:29:00	T
12	d29c1e65-8eff-4459-bbce-a5f39714e011	3	0	43	0	2017-09-15 19:29:00	T
13	d29c1e65-8eff-4459-bbce-a5f39714e011	3	0	44	0	2017-08-15 19:29:00	T
14	929981dc-d19d-42df-8e7e-f6896239ff54	3	0	45	0	2017-12-25 19:29:00	T
15	02f28cbf-ef25-463f-9c77-901ae627ac54	3	0	46	0	2017-11-09 19:29:00	T
16	02f28cbf-ef25-463f-9c77-901ae627ac54	3	0	47	0	2017-11-16 19:29:00	T
17	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	3	0	48	0	2017-11-09 19:29:00	T
18	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	3	0	49	0	2017-11-17 19:29:00	T
19	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	3	0	50	0	2017-11-08 19:29:00	T
20	74e14b79-514a-4db0-9de5-61f995daf9bf	3	0	51	0	2017-12-15 19:29:00	T
21	74e14b79-514a-4db0-9de5-61f995daf9bf	3	0	52	0	2017-11-17 19:29:00	T
22	cdd5a6a2-cb0d-409d-8cf8-95593d888423	3	0	53	0	2017-11-14 19:29:00	T
23	cdd5a6a2-cb0d-409d-8cf8-95593d888423	3	0	54	0	2017-11-11 19:29:00	T
24	1a698814-ab5b-467e-b243-ece7a100e291	3	0	55	0	2017-11-12 19:29:00	T
25	127317d1-c913-403f-ac63-dbfb4b6c0d3a	3	0	56	0	2017-11-15 19:29:00	T
26	5bece49d-76a0-4184-88f7-9e420fec1666	3	0	57	0	2017-11-12 19:29:00	T
27	aa32eb82-d698-468e-8c0b-dc3a6bebfc12	2	0	19	16	2018-01-17 16:34:00	T
28	aa32eb82-d698-468e-8c0b-dc3a6bebfc12	2	0	21	17	2018-01-11 16:42:00	T
29	eb5d625e-aa8f-4709-a296-f92b1df0dd3e	2	0	22	18	2017-12-17 17:13:00	T
30	e40ee71a-e3de-4b33-a877-0cc91fdcb337	2	0	22	19	2018-01-02 19:06:00	T
31	5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9	2	0	36	37	2017-12-17 20:05:00	T
32	174b4461-5b84-40da-bba1-71a45bf0d6a3	2	0	37	38	2018-01-19 20:16:00	T
33	b6072fcd-7589-46f6-841a-d3c2adb8323d	2	0	37	39	2018-02-17 20:31:00	T
34	4f3518e8-abf7-457f-b6ab-15e37e99b81b	2	0	38	40	2018-02-10 20:45:00	T
35	4f3518e8-abf7-457f-b6ab-15e37e99b81b	2	0	39	41	2017-12-19 20:53:00	T
36	75245e73-98c1-4828-a7b7-77c0eefa3d7d	2	0	39	42	2018-02-17 20:57:00	T
37	cef53707-04ea-45d5-b7ac-c86f87aa625d	2	0	40	43	2018-02-01 21:06:00	T
38	cef53707-04ea-45d5-b7ac-c86f87aa625d	2	0	41	44	2018-03-17 21:11:00	T
39	d29c1e65-8eff-4459-bbce-a5f39714e011	2	0	42	45	2018-01-08 21:40:00	T
40	d29c1e65-8eff-4459-bbce-a5f39714e011	2	0	43	46	2018-01-01 21:54:00	T
41	d29c1e65-8eff-4459-bbce-a5f39714e011	2	0	44	47	2017-12-27 21:57:00	T
42	929981dc-d19d-42df-8e7e-f6896239ff54	2	0	45	48	2017-11-17 22:05:00	T
43	929981dc-d19d-42df-8e7e-f6896239ff54	2	0	37	49	2018-01-10 22:05:00	T
44	02f28cbf-ef25-463f-9c77-901ae627ac54	2	0	46	50	2018-01-10 22:13:00	T
45	02f28cbf-ef25-463f-9c77-901ae627ac54	2	0	47	51	2018-01-18 22:24:00	T
46	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	2	0	48	52	2018-01-10 22:27:00	T
47	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	2	0	49	53	2018-02-17 22:31:00	T
48	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	2	0	50	54	2018-02-07 22:33:00	T
49	3af7e93a-7873-41fb-b991-43b89126f635	2	0	49	55	2018-01-19 22:40:00	T
50	74e14b79-514a-4db0-9de5-61f995daf9bf	2	0	51	56	2018-01-28 22:46:00	T
51	74e14b79-514a-4db0-9de5-61f995daf9bf	2	0	52	57	2018-01-20 22:49:00	T
52	cdd5a6a2-cb0d-409d-8cf8-95593d888423	2	0	53	58	2017-11-17 23:08:00	T
53	cdd5a6a2-cb0d-409d-8cf8-95593d888423	2	0	54	59	2018-01-29 23:12:00	T
54	1a698814-ab5b-467e-b243-ece7a100e291	2	0	55	60	2018-03-17 23:17:00	T
55	127317d1-c913-403f-ac63-dbfb4b6c0d3a	2	0	56	61	2018-02-17 23:21:00	T
56	ad3ffb5d-95b1-4528-a0a5-92591a21de34	2	0	56	62	2018-02-17 23:24:00	T
57	5bece49d-76a0-4184-88f7-9e420fec1666	2	0	57	63	2018-02-17 23:40:00	T
58	02f28cbf-ef25-463f-9c77-901ae627ac54	1	24	0	0	2018-02-19 22:56:00	T
59	127317d1-c913-403f-ac63-dbfb4b6c0d3a	1	26	0	0	2018-01-19 22:56:00	T
60	127317d1-c913-403f-ac63-dbfb4b6c0d3a	1	18	0	0	2018-03-11 22:56:00	T
61	174b4461-5b84-40da-bba1-71a45bf0d6a3	1	34	0	0	2018-03-12 22:56:00	T
62	1a698814-ab5b-467e-b243-ece7a100e291	1	30	0	0	2018-03-16 22:56:00	T
63	1a698814-ab5b-467e-b243-ece7a100e291	1	28	0	0	2018-02-01 22:56:00	T
64	b6072fcd-7589-46f6-841a-d3c2adb8323d	1	29	0	0	2018-03-19 22:56:00	T
65	5bece49d-76a0-4184-88f7-9e420fec1666	1	19	0	0	2018-02-01 22:56:00	T
66	ad3ffb5d-95b1-4528-a0a5-92591a21de34	1	20	0	0	2018-01-02 22:56:00	T
67	75245e73-98c1-4828-a7b7-77c0eefa3d7d	1	31	0	0	2018-03-12 22:56:00	T
68	b6072fcd-7589-46f6-841a-d3c2adb8323d	1	39	0	0	2018-01-01 22:56:00	T
69	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	1	14	0	0	2018-03-06 22:56:00	T
70	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	1	16	0	0	2017-09-06 22:56:00	T
71	75245e73-98c1-4828-a7b7-77c0eefa3d7d	1	18	0	0	2017-03-06 22:56:00	T
72	b6072fcd-7589-46f6-841a-d3c2adb8323d	1	27	0	0	2018-01-09 22:56:00	T
73	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	1	25	0	0	2018-03-09 22:56:00	T
74	127317d1-c913-403f-ac63-dbfb4b6c0d3a	1	36	0	0	2017-12-19 22:56:00	T
75	127317d1-c913-403f-ac63-dbfb4b6c0d3a	1	34	0	0	2017-11-19 22:56:00	T
76	174b4461-5b84-40da-bba1-71a45bf0d6a3	1	34	0	0	2018-03-19 22:56:00	T
77	cef53707-04ea-45d5-b7ac-c86f87aa625d	1	33	0	0	2018-10-16 22:56:00	T
78	127317d1-c913-403f-ac63-dbfb4b6c0d3a	1	40	0	0	2018-03-16 22:56:00	T
79	3af7e93a-7873-41fb-b991-43b89126f635	1	41	0	0	2018-03-11 22:56:00	T
80	e8deb3b8-da18-4ab7-ad8f-812e65aa6fb8	4	0	60	67	2018-09-16 01:11:40.587	T
81	e8deb3b8-da18-4ab7-ad8f-812e65aa6fb8	1	31	0	0	2018-09-16 01:12:34.81	T
83	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	53	58	2018-09-16 01:13:20.793	T
85	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	58	64	2018-09-16 01:13:34.78	T
86	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	61	69	2018-09-16 01:13:37.73	T
87	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	55	60	2018-09-16 01:13:39.83	T
88	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	49	55	2018-09-16 01:13:45.193	T
89	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	44	47	2018-09-16 01:13:49.8	T
90	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	42	45	2018-09-16 01:13:54.3	T
91	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	40	43	2018-09-16 01:14:03.043	T
92	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	22	18	2018-09-16 01:14:06.647	T
93	c9082805-5404-423c-a84e-768974d55a9a	2	0	37	76	2018-10-27 08:10:47.853	T
94	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	37	76	2018-10-27 08:11:25.62	T
95	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	37	76	2018-10-27 08:11:25.62	T
96	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	37	76	2018-10-27 08:11:25.62	T
97	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	37	76	2018-10-27 08:11:25.62	T
98	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	59	66	2019-01-10 05:18:25.787	T
99	fe8c5539-5945-4cfb-9892-6cb4d3935c90	4	0	39	41	2019-01-15 20:40:10.177	T
100	434decd7-89d9-422d-853c-22d3eca887d1	2	0	57	77	2020-12-05 09:31:19.91	T
\.


--
-- Data for Name: AnswerLikes; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AnswerLikes" ("Id", "AppUserId", "AnswerId", "TRIAL301") FROM stdin;
1	e8deb3b8-da18-4ab7-ad8f-812e65aa6fb8	67	T
3	fe8c5539-5945-4cfb-9892-6cb4d3935c90	58	T
5	fe8c5539-5945-4cfb-9892-6cb4d3935c90	64	T
6	fe8c5539-5945-4cfb-9892-6cb4d3935c90	69	T
7	fe8c5539-5945-4cfb-9892-6cb4d3935c90	60	T
8	fe8c5539-5945-4cfb-9892-6cb4d3935c90	55	T
9	fe8c5539-5945-4cfb-9892-6cb4d3935c90	47	T
10	fe8c5539-5945-4cfb-9892-6cb4d3935c90	45	T
11	fe8c5539-5945-4cfb-9892-6cb4d3935c90	43	T
12	fe8c5539-5945-4cfb-9892-6cb4d3935c90	18	T
17	fe8c5539-5945-4cfb-9892-6cb4d3935c90	66	T
18	fe8c5539-5945-4cfb-9892-6cb4d3935c90	41	T
\.


--
-- Data for Name: Answers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Answers" ("Id", "Content", "CreatedDate", "QuestionId", "AppUserId", "UpdatedDate", "IsDeleted", "TRIAL304") FROM stdin;
16	<p>There is a really great part of the film "Gran Torino" where he brings the kid into his garage. The kid sees all of his tools and is astonished, saying "I can鈥檛 afford to buy all this stuff" to which Eastwood鈥檚 character replies "Even a bonehead like you can understand that a man acquires this over a period of 50 years". But he then gives the kid some vice-grips, wd-40, and duct tape, and says he could keep it, and that he could solve a lot of household issues with those.</p><p>Despite the rudeness of his statement, there is truth to it, especially for programmers.</p><p>I was a beginner, not so long ago, and I remember thinking to myself the same thing "how am I gonna learn all this stuff, I don鈥檛 have time for it and there is so much!"</p><p>The truth is, you acquire each skill / tool / language over time. Certainly, it is harder for us as programmers since Libraries and Frameworks seem to spring out of the ground and have surprising complexity, but it gets easier to learn new things, and there is familiarity that grows with use. As you progress in your career, you will have to learn new things, new skills, new tools, but your ability to use the old ones doesn鈥檛 just disappear, and some of the underlying structures of all that we do as developers does not move as quickly as the languages do.</p><p>So essentially, time is how. It just takes time. When you look at the entirety of the full range of tools in the tech stack, thinking that is what you have to take on, you鈥檒l get buried. But if you focus on small parts of it and acquire ability in that chosen discipline, you can then move on to another, and add it to your tool belt.</p>	2018-01-05 16:34:00	19	aa32eb82-d698-468e-8c0b-dc3a6bebfc12	\N	f	T
17	<p>To start, how much do you know?</p><p>What is this? Is it the advertisements? Building the buildings? Owning a restaurant? What?</p><p>Do you want to do Web Development or build software? Do you want to do Front End programming or Back End? What industry do you want to work in? Games? Biotech? SaaS?</p><p>Do you want to learn how to program for yourself and to just have fun?</p><p>Answer these questions first, and then look for resources. As long as Net Neutrality reigns, there are a plethora of free resources for beginners. These two</p><p><a href="https://www.codecademy.com/" target="_blank">Codecademy - learn to code, interactively, for free</a>,</p><p><a href="https://www.freecodecamp.org/" target="_blank">Learn to code with free online courses, programming projects, and interview preparation for developer jobs</a>&nbsp;are just a few.</p><p>After doing some coding in these free resources, you need to REFLECT. Is this the type of work you enjoy? Is it something you want to do? Could you safely devote time and resources to it and push yourself very hard towards this goal?</p><p>If you say yes to this, and your true aim is to learn to program quickly, then maybe a code bootcamp is right for you. Research to find a good one. Keep in mind they will cost you money in the range of 10k, and keep in mind that you pay for what you get. You will need to be dedicated. I am talking going to classes, paying attention, asking questions, leaving the classes and going home and coding and reading and using free resources and coding and practicing and making side projects that are not homework and coding and getting on to Quora and Stack Exchange and Github and Codewars and&nbsp;<a href="http://codepen.io/" target="_blank">CodePen</a>&nbsp;and working your ass off.</p><p>But all of that is if you want a quick turn around, a couple of months.</p><p>If you have the luxury of a little more time, Codeschool and Pluralsight would be good recommendations and way way cheaper than a code bootcamp. You can learn at your own pace, but will still have to work hard. And you鈥檒l have questions these sites won鈥檛 answer, so you鈥檒l probably have to buy books or find a programmer you could bug without them minding too much. But you still need to build projects while learning, build side projects, passion projects, build, look at best practices, and learn.</p><p>The world of programming is an accessible one, but it is vast and complex and can be daunting. If a speedy path to proficiency is truly your goal, you will need to spend much of your time, weekends too, devoted to it.</p><p>Good luck.</p>	2018-01-09 16:42:00	21	aa32eb82-d698-468e-8c0b-dc3a6bebfc12	2018-01-17 16:46:00	f	T
18	<p><strong><span class="ql-cursor">锘?/span>Space ship battle scenes.</strong></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-97f09ff2907144af03dcd267d00e7e7f.webp"></p><p>Just consider the following.</p><p>Two thousand years ago, humans used bows and arrows as their primary long - range weapon of choice.</p><p>One thousand years ago, humans could fire projectiles to greater distances, with payloads of greater masses, thanks to the catapult.</p><p>Seven hundred years ago, humans increased their distance, their payload, and decrease their reloading time, by inventing the cannon.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-dd191726557c0c30a22bf82bf648df1f.webp"></p><p>Seventy five years ago, the first weaponized rocket was mass produced for a modern conflict, the V-2 rocket, which had a range of 300 km, with a payload that weighed about a ton.</p><p>Nowadays, in 2018, we have mass produced ICBM鈥檚 that can reach targets over 5,500 km, and long range artillery that can accurately hit a target 40km away, firing six rounds a minute.</p><p><em>WWII battleship firing its cannons.</em></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-60b6d4466e15ee19e78d3d2feec07932.webp"></p><p>Now, humanity is at least another thousand years away from engaging in any space wars, giving us plenty of time to perfect our ranged weapons.</p><p>When space wars do happen, I鈥檇 imagine we would be bombing each other half way across the galaxy, using weapons with ranges in the millions of km, instead of engaging in close range combat, losing thousands of ships.</p><p>I mean, if you know where the enemy fleet is, since you hyper warped right into their position and engaged in close combat, why don鈥檛 you just launch a super plasma rail gun at them from 100,000 km away and save everyone the trouble?</p>	2017-12-17 17:13:00	22	eb5d625e-aa8f-4709-a296-f92b1df0dd3e	2018-01-17 17:21:00	f	T
19	<p><strong>Inception</strong></p><p>Undoubtedly, one of the greatest movies ever made. My observation may be inaccurate so feel free to correct me. Understanding the plot itself was a difficult task. Had to see the movie several times to get a hold of it ( phew ).</p><p>So here we go</p><p>Remember this safe scene in the climax ?</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-92f17ba7a4843de767a078841ef7c409.webp"></p><p>Can you read it ? the numbers ?</p><p>Yes ? Good !!</p><p>This happens in a dream within a dream right ?</p><p>well ..</p><p>as a matter of fact you<strong>&nbsp;can鈥檛 read&nbsp;</strong>while you are dreaming.</p>	2017-01-24 19:06:00	22	e40ee71a-e3de-4b33-a877-0cc91fdcb337	\N	f	T
37	<p>Well aside from technology that has come out like iPhones like has in all countries in no particular order:</p><ol><li>Christchurch had its major earthquake and is still a recovering city.</li><li>Smoking has got much much stricter laws. Smoking rates from what I understand keep getting lower and lower each year.</li><li>Gay and lesbian marriage has become legal.</li><li>The arts, film production scene and everything related to that has become so much better.</li><li>Consumer electronics like TVs etc have all got cheaper.</li><li>Internet speeds have got much better. There are a lot of people now that can access 500 mbps upload and 1000 mbps download speeds.</li></ol><p>There is many more as well things that have changed as well.</p>	2018-01-12 20:05:00	36	5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9	\N	f	T
38	<p>I visited when I was 18. I fell in love with the country, its beauty and vibrancy. I then met an amazing young man and fell in love with him too.</p><p>I couldn鈥檛 live without either, so I eventually married the man and emigrated to NZ.</p><p>Ten years, three kids, a dog and cat later, I鈥檓 still here and am an NZ citizen. In a way, NZ has become an intrinsic part of who I am and what makes me happy.</p>	2018-01-11 20:16:00	37	174b4461-5b84-40da-bba1-71a45bf0d6a3	\N	f	T
39	<p>We walked to this waterfall on a beautiful sunny day. We passed a waterfall to get to it!</p><p>New Zealand is everything they say it is and more.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-9f2dac3f40d077451341d5787f9e711b.webp"></p><p><br></p>	2018-01-16 20:31:00	37	b6072fcd-7589-46f6-841a-d3c2adb8323d	\N	f	T
40	<p>Infrastructure side is getting a lot better all over China. Big city like Shanghai has all the state of art stuff, household fiber is now 200MB avg and 500MB for contract renewal customer. 4G is so popular, and data plan wars running among the 3 major carrier are at a new high 鈥?my secondary data sim from ChinaTelecom is now upgraded to "Unlimited Plan" without pay a penny extra. Public wifi? Who needs public wifi if you have big 4G data plan?... Internet egress at global side is terrible, both due to GFW and bandwidth limitation (public internet hopping at Japan or Hongkong via NTT or PCCW etc., all cheap/free peers, low quality). Cisco has been essentially kicked out from backbone infrastructures, only Huawei and ZTE etc. SOE products.</p><p>Be noted 4G has 3 different standard in China, each carrier use one of them. All standard in active use has certain level of modification vs international standard. E.g. the TD-LTE has different spectrum bands vs other LTEs globally. In short 鈥?if your phone is not bought in China, it is very likely it won鈥檛 achieve the full capability of local 4G network.</p><p>If you roam in China with your home sim, be noted your data traffic will be first directed back home country APNs before hits to internet. It will be slow if you are not from HK or Japan or nearby countries.</p><p>GIS is completely different monster. The China government knows how important to control the map and surveying activities. I have been dealing with government (National Surveying Bureau and National Security Bureau) on an unintended incident that involves embedded GIS at mobility solution鈥?long story鈥? In short they know what they are doing. It is very inconvenient for internet company use map or GPS, but the market accepted the rule and played out ok. Anyway, I don鈥檛 fancy to bring my engineer to talk with a group of uniforms again. TianDiTu is a lousy front end tool with high quality map behind. Those map is sold by Survey Bureau鈥檚 child companies (~4 SOEs) to industry. Check out the licensing party under Baidu Map, it鈥檚 one of the SOEs I鈥檓 talking. Again, all map for public is skewed. The algorithm to skew GPS on a skewed map is provided by SOEs as well, only to china based companies and devices. Simple test 鈥?get a foreign GPS device (eg a GPS watch), read the raw GPS coordinates, write down the coordinates, load a local digital map or similar, put in your coordinates 鈥?you are 500 meter or 1KM away from where you really are??? That鈥檚 the distortion hidden in the maps, except you don鈥檛 know the algorithm.</p><p>Internet commerce (B2C, B2B, O2O etc etc.) China is very advanced, probably 5 yrs ahead of US, who is 10 years ahead of Europe.</p><p>Internet Payment, Cashless Society鈥?same, the top in the world.</p><p>Internet Security鈥?very low, both awareness and investment. Technology is also very low.</p><p>Cloud 鈥?lots of IaaS, little development on PaaS, small scale SaaS startups. Miles away from global standard.</p><p>AI 鈥?hot, but lack of knowhow. Most people are busy on apply AI into use, not the AI engine or data mode itself.</p><p>鈥?</p><p>Tired on typing. Will stop here.</p>	2017-12-28 20:45:00	38	4f3518e8-abf7-457f-b6ab-15e37e99b81b	\N	f	T
41	<p>Traditional Festivals are mainly celebrated on a certain date on a calendar. China's lunar calendar&nbsp;<a href="https://en.m.wikipedia.org/wiki/Chinese_calendar" target="_blank">Chinese calendar - Wikipedia</a>&nbsp;has been adopted by Korea and Japan from 2000 years ago, together with a lot of festival tradition. 200 years ago Japan switched to Gregorian calendar completely, since then the lunar fastivals are mostly lost in Japan. Korea till today still keeps lunar calendar like China does, and very much most of traditional Festivals celebrated in slightly different customs.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-33a561b5d76736e32d567a90a8737840.webp"></p><p><a href="http://www.korea.net/AboutKorea/Korean-Life/Festivals" target="_blank">Festivals, Celebrations and Holidays</a></p><p><a href="https://en.m.wikipedia.org/wiki/List_of_Korean_traditional_festivals" target="_blank">List of Korean traditional festivals - Wikipedia</a></p><p><a href="https://en.m.wikipedia.org/wiki/Japanese_festivals" target="_blank">Japanese festivals - Wikipedia</a></p>	2018-01-01 20:53:00	39	4f3518e8-abf7-457f-b6ab-15e37e99b81b	\N	f	T
42	<p>I鈥檒l also include Vietnam in this answer, since all 4 of them share 90% of traditional holidays.</p><p>Since China, Korea, Japan, and Vietnam all shared the lunisolar calendar (derived from China) traditionally, they would obviously share holidays of that calendar. Each nation evolved into different rituals, customs, names, etc, but the spirits of those calendars still remain.</p><p>Even Japan, which has completely gotten rid of the lunisolar calendar, still followed these festival traditions.</p><p>But of course, not every festival is shared by all 3. Sometimes, one just didn鈥檛 catch on and died in the process.</p><p>(I鈥檓 not entirely 100% on this, so please correct me if needed)</p><p><strong>1. New Year鈥檚 Eve</strong></p><p>Chinese: 闄ゅ</p><p>Japanese: 澶ф櫐鏃?/p><p>Korean: 靹ｋ嫭攴鸽瘣</p><p>Vietnamese: 膼锚m Giao Th峄玜</p><p><strong>2. New Year</strong></p><p>Chinese: 鏄ョ瘈</p><p>Japanese: 鍏冩棩</p><p>Korean: 靹る偁</p><p>Vietnamese: T岷縯 Nguy锚n 膼谩n</p><p><strong>3. Lantern Festival</strong></p><p>Chinese: 鍏冨绡€</p><p>Japanese: 灏忔鏈?/p><p>Korean: 雽€氤措</p><p>Vietnamese: T岷縯 Th瓢峄g Nguy锚n</p><p><strong>4. Moon Festival</strong></p><p>Chinese: 涓绡€</p><p>Japanese: 鏈堣</p><p>Korean: 於旍劃</p><p>Vietnamese: T岷縯 Trung Thu</p><p><em>鈥?and like, 10 more that I can鈥檛 really name all鈥?/em></p>	2018-01-16 20:57:00	39	75245e73-98c1-4828-a7b7-77c0eefa3d7d	\N	f	T
43	<p>Before trying to answer that question, let me tell you a little bit about how does it work. This address is used to establish a connection to the same computer used by the end-user. When we deal with IPv6 address, it鈥檚 defined using the connotation of ::1. As IPv6 addresses take over, localhost will be more commonly know as 0:0:0:0:0:0:0:1.</p><p><strong>How does 127.0.0.1 work? Why is it called so?</strong></p><p>Very often developers use 127.0.0.1 to test their applications. When you try to establish a network connection to the 127.0.0.1 loopback address, it works in the same manner as making a connection with any remote device. However, it avoids connection to the local network interface hardware.</p><p>But, why does the localhost IP address starts with 127? Well, 127 is the last network number in a class A network. It has a subnet mask of 255.0.0.0. So, the first assignable address in the subnet is 127.0.0.1.</p><p>However, if you use any other numbers from the host portions, it should work fine and revert to 127.0.0.1. So, you can ping 127.1.0.1 if you like.</p><p>You might also ask why the last network number was chosen to implement this. Well, the earliest mention of 127 as loopback dates back to&nbsp;<a href="https://tools.ietf.org/html/rfc990#page-6" target="_blank">November 1986 RFC 990</a>. And, by 1981, 0 and 127 were the only reserved Class A networks.</p><p>The class A network number 127 is assigned the "loopback" function, that is, a datagram sent by a higher level protocol to a network 127 address should loop back inside the host. No datagram "sent" to a network 127 address should ever appear on any network anywhere.</p><p>As 0 was used for pointing to a specific host, 127 was left for loopback. Some would also call it more sensible to choose 1.0.0.0 for loopback, but that was already given to BBC Packet Radio Network.</p>	2018-01-17 21:06:00	40	cef53707-04ea-45d5-b7ac-c86f87aa625d	\N	f	T
44	<p>Ok my recommendation is to start with the basics and work you way up. If the answer to any of these questions is NO then trouble shoot that issue first.</p><ol><li>Are both server powered on and connected to a network and can you see the internet from both servers?</li><li>Can you ping one server from the other and visa versa. Ping may be blocked so this is sometimes a non-issue. I normally use mtr (or WINMTR) which is a free application that will run multiple trace routes between two servers. You will get something like</li></ol><p><img src="data:image/png;base64,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"></p><ol><li class="ql-indent-1">Check for packet loss</li><li class="ql-indent-1">check for long times between the servers and where does it occur. Anything over 100ms increase between routers is a possible bottle neck.</li></ol><p>Can you get to the port on the server you are trying to connect to. You can use nmap to check what ports see open on one server from the other or you can use telnet to connect to a specific port on another server. If you can鈥檛 reache the port you need then check the firewall.</p><p><br></p>	2018-01-09 21:11:00	41	cef53707-04ea-45d5-b7ac-c86f87aa625d	2018-01-17 21:28:00	f	T
45	<p>There is no doubt that robots are&nbsp;<em>"just machines"</em>, but that definitely doesn鈥檛 mean they are not to be feared.</p><p>With this in mind we can ask the question,&nbsp;<em>"why should we fear machines?".</em></p><p>The year is 1850 and you work as a skilled handweaver in Manchester, England. However, almost overnight, the rise of the power loom, a mechanized loom, a&nbsp;<em>machine,&nbsp;</em>seriously devalues your work and so your wages plummet. You most definitely fear the machine.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-597de944dd03fbee4afd1f4d6f471122.webp"></p><p><br></p><p>Next, let us place ourselves on the battlefields of World War 1, where both sides began their first deployments of the tank. Revolutionising warfare, these massive&nbsp;<em>machines</em>no doubt struck fear into any rational soldier, however, unlike the previous example, the soldier fears for his life not for his livelihood and wages.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-7265ee3c3559b0aed2ab4831c9f83b75.webp"></p><p><br></p><p>Fast forward to today where&nbsp;<em>machine,&nbsp;</em>though awesomely more advanced, still remain fundamental threats to humans in several way. The modern fear of robots can be seen as modern equivalents of our two examples above.</p><p>Firstly, there is a growing fear that with the rise of Artificial Intelligence, many low skilled (and in fact even high skilled jobs as law, medicine etc.) could be replaced in the very near future. Though much of this fear can be amplified by sensationalist reporting, there are undeniable examples of places where AI will take over. For example, with self driving trucks the trucking industry will be decimated. That鈥檚 over 3.5 million&nbsp;truck drivers who鈥檚 jobs are devalued overnight. Not to mention the millions of employees who are indirectly linked to the trucking industry in road side diners, mechanics etc. There is definite rational for fear.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-30955a43baa67e6c2ea77c88c2c9c7a1.webp"></p><p>Secondly, a point raised frequently by Elon Musk<a href="https://www.quora.com/topic/Artificial-Intelligence#JwtwP" target="_blank">[3]</a>&nbsp;, is the use of these modern machines in warfare. Artificially Intelligent weapons will fundamentally change warfare by disconnecting the inflictor and inflected of violence. For example, if&nbsp;<em>"smart"&nbsp;</em>drones<em>&nbsp;</em>were combined<em>&nbsp;</em>with the present frequency of drone strikes in the Middle East, it is easy to see what Musk and many others fear.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-cc2b41f640a92fb7fea77c8474ca2928.webp"></p><p><br></p><p>However, this question would be incomplete without the disclaimer that though we may fear a robotic future it is not the machines that we should fear, but their social effects and the handling of their development. There is no doubt that machines drive forward humanity and effect society positively. The very period in which the power loom decimated skilled workers wages was one of unprecedented prosperity in England. AI today is being used to help in hospitals, self driving cars will make roads safer. It is through human decision making that we can utilise these machines for the right reasons and provide sufficient support for those whose jobs will be devalued.</p><p>If we are entering a&nbsp;<em>"new Industrial Revolution",</em>&nbsp;then we should not fear the machines but instead be socially critical of the people and the motives behind them.</p><p><strong>Footnotes</strong></p><p><a href="https://www.quora.com/topic/Artificial-Intelligence#cite-tPdWO" target="_blank">[1]&nbsp;</a><a href="http://www.independent.co.uk/life-style/gadgets-and-tech/news/facebook-artificial-intelligence-ai-chatbot-new-language-research-openai-google-a7869706.html" target="_blank">Facebook robots shut down after they talk to each other in language only they understand</a></p><p><a href="https://www.quora.com/topic/Artificial-Intelligence#cite-XRUhG" target="_blank">[2]&nbsp;</a><a href="http://www.trucking.org/News_and_Information_Reports_Industry_Data.aspx" target="_blank">American Trucking Associations</a></p><p><a href="https://www.quora.com/topic/Artificial-Intelligence#cite-JwtwP" target="_blank">[3]&nbsp;</a><a href="https://www.theguardian.com/technology/2017/aug/20/elon-musk-killer-robots-experts-outright-ban-lethal-autonomous-weapons-war" target="_blank">Elon Musk leads 116 experts calling for outright ban of killer robots</a></p>	2017-12-17 21:40:00	42	d29c1e65-8eff-4459-bbce-a5f39714e011	\N	f	T
46	<p>The label of being the&nbsp;<em>"best career"&nbsp;</em>that is attributed to Machine Learning is definitely undeserved. It all stems from one word;&nbsp;<strong>hype</strong>.</p><p>"WHAT!" scream the millions of deep learning enthusiasts and ML engineers waiting in the wings.</p><p>Let me explain before pitchforks are raised.</p><p>There is no doubt Machine Learning is an interesting field to be in. It鈥檚 not only intellectually stimulating, its results appear near magic to the general public. Machines beating the world鈥檚 best at Go, self driving cars, Snapchats face filters. It鈥檚 sexy to be a Machine Learning engineer, to be involved in the very real wave of new technologies.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-9438e52674d537118aec7f5273316fb1.webp"></p><p><br></p><p>But there comes a point where the public greatly over values a profession. News articles such as the New York Times鈥?listing salaries of 300 - 500k for AI professionals lead young graduates to believe Machine Learning is the best way to make big money. The problem begins when people start to pursue a career in Machine Learning&nbsp;<em>because</em>&nbsp;it鈥檚 the supposed&nbsp;<em>"best career"</em>. Combine this with the fact the field is so vaguely defined, encompassing everything from Data Engineers to statisticians to quantitative analysts with Python and you have a misinformed public and a ballooned valuation.</p><p>Since the field is so young, there are massive shortages in graduates educated with the relevant skills. As a result, salaries have of course rocketed. However, just a few years behind, the Universities of the world are starting more and more&nbsp;<em>"Data Science"</em>&nbsp;and&nbsp;<em>"Machine Learning"&nbsp;</em>degree programmes. Surely, this is the answer to the job shortage problem? Not quite. Because the field鈥檚 value is so distorted universities across the globe are starting sub par programmes which receive buckets of applications from (genuinely interested) students. These programmes make nice money for the university but only make the problem worse. Thousands of graduates now qualified as&nbsp;<em>"Machine Learning experts"</em>&nbsp;are under equipped for the task necessary, further clouding the definition of Machine Learning field. You can bet that in the years to come, when ML has stabilised, so too will the salaries and only those who are truly interested in the field will find their jobs fulfilling.</p><p>The real message I want to communicate in this answer is not that Machine Learning is overly hyped. My main message is that there is no such thing as&nbsp;<em>"the best career".&nbsp;</em>There is only&nbsp;<em>"the best career&nbsp;</em><strong><em>for you</em></strong><em>"</em>. The most important thing to remember (just like for any job, however publicly&nbsp;<em>valuable)&nbsp;</em>is to honestly assess whether you have a genuine interest in the subject. Do you love playing with data and ML algorithms? Do you find the underlying math beautiful? Machine Learning is a fascinating field and, if it's right for you, it's rewarding, fulfilling and you can make some decent money doing it. But it's important to isolate the publics opinions from your own.</p><p><strong>Footnotes</strong></p><p><a href="https://www.quora.com/profile/Brian-Regan-11#cite-tMVRx" target="_blank">[1]&nbsp;</a><a href="https://www.nytimes.com/2017/10/22/technology/artificial-intelligence-experts-salaries.html" target="_blank">Tech Giants Are Paying Huge Salaries for Scarce A.I. Talent</a></p>	2018-01-17 21:54:00	43	d29c1e65-8eff-4459-bbce-a5f39714e011	2018-01-17 21:54:00	f	T
47	<p>Of movies in cinemas right now (early November 2017), I would say Denis Villeneuve鈥檚&nbsp;<a href="https://www.rottentomatoes.com/m/blade_runner_2049/" target="_blank">Blade Runner 2049</a>&nbsp;is one of the best, even if you have never seen the original.</p><p>Visually, Roger Deakins could be in for a 14th Oscar nomination for his cinematography and perhaps his first win. The awesome cityscapes, magnificent spectra of colors and entrancing shots really make a film that is worth catching in a theater.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-c331aba6c87f88d6521c3d2a3a1e26ca.webp"></p><p>Hans Zimmer and Benjamin Wallfisch created a soundtrack that was one of the most impressive elements of the film for me. The music here is not just a background element while the story plays out on screen but is as emotionally draining and visceral an element as the plot. Again, a theater with a quality sound system completely obliterates any home sound system in terms of experience.</p><p>The actors do a fantastic job too, especially Ryan Gosling, adopting a role reminiscent of his role in the film&nbsp;<a href="http://www.imdb.com/title/tt0780504/" target="_blank">Drive</a>, if you鈥檝e seen it.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-d2c9566eed52ea2366bad4163e26e4a0.webp"></p><p>The story itself, though science fiction, will appeal to most and poses some fantastic and relevant questions about reality, consciousness and love. I won鈥檛 comment about it here because I think going into this movie with as little information as possible will pay the biggest dividends.</p><p>For me, especially for a "reboot", the film was one of the most original and freshest movies of the year.</p>	2018-01-11 21:57:00	44	d29c1e65-8eff-4459-bbce-a5f39714e011	\N	f	T
48	<p>About the only negative of my experience in China was the heavy pollution in Beijing. Other than it was the best trip I ever did. It was completely different from my expectations. I was pretty much free to do anything and go wherever I wanted. I was treated with courtesy, respect and friendliness by almost everyone. It's a fascinating place and I would go back in a heartbeat.</p>	2018-01-10 22:05:00	45	929981dc-d19d-42df-8e7e-f6896239ff54	\N	f	T
49	<p>We were very impressed with the friendliness of the average people. Several times people approached us and said something like : You are Yanks aren't ya? There is a tourist /historical site up the street that you really ought to see. One time, we purchased a bowl from an artisan, he noticed that we had a camper van. He then said that we were welcome to camp on his land for free. One of the continuing problems we had, was stopping on the side of the road to take a leak. People kept stopping to offer help because they thought we were having car problems.</p><p>Really great people.</p>	2018-01-16 22:05:00	37	929981dc-d19d-42df-8e7e-f6896239ff54	\N	f	T
50	<p><img src="https://qph.fs.quoracdn.net/main-qimg-c7c3123bbee9a5b4ee335b6d17237501.webp"></p><p><strong>How Long Does it Take to Change Your Body Using Total Gym?</strong></p><p>You got the equipment -- now you're ready to get started with your Total Gym and to start seeing results. If you've invested in one of the Total Gym's products that promise to train and tone most of the muscles of your body, what you have is essentially a piece of strength training equipment, with the option of adding some cardio as well. Strength training is an essential part of a healthy exercise routine, and by using the equipment wisely, you may be surprised to see results very quickly.</p><p><strong>Results from Strength Training</strong></p><p>The surprising thing about strength training is that people tend to see results almost right away. When you first start doing resistance exercises, as you do with the Total Gym, you'll very likely see results within the first few weeks. During that initial period, your body is undergoing neural adaptations that cause the muscles you're using to "relearn" how to be stronger and bigger. After that initial period, you'll likely continue building muscle, but it will be less noticeable compared to the changes you saw during the first few weeks. Still, it's important to stick with one routine for at least a few months to really see what that type of workout can do for you, reminds personal trainer Jonathan Ross, writing for the American Council on Exercise.</p><p><strong>The Time Factor</strong></p><p>As with all workout routines, the results you'll see will be due, at least in part, to the amount of effort you put in. If you're using the Total Gym five days a week for 30 minutes a day, you're naturally going to see changes to your body faster than if you only used it twice a week for a few minutes at a time. In the world of fitness, several factors affect your training results, including frequency, intensity, time and type of exercise. Working out more frequently and for a longer period matters. So does the intensity of the workout. If you're not using an amount of resistance that causes your muscles to feel fatigued toward the end of the set, you're probably not putting in enough effort and you should increase the resistance on the machine.</p><p><strong>Adaptation</strong></p><p>Finally, the types of exercise you do also matter. Over time, your body becomes adapted to the demands you're putting upon it, and will make adaptations that can result in seeing fewer positive results. The Total Gym offers a myriad of exercises, so about every four weeks, change your routine and do different exercises. That will help you continue to see changes to your body. Also, keep in mind that every body is different. Some people will tend to gain muscle or shed fat faster than others -- so compare your progress only to yourself and not to someone with a totally different body type.</p><p><strong>Other Factors</strong></p><p>When you want to see changes to your body, strength training is just one component. Aerobic exercise is key not just to burning calories, but to increasing the amount of oxygen your body can use -- which can lead to positive body changes such as breathing and moving around more easily. If your goal is to lose weight, you'll need to either take advantage of the cardio exercise suggestions offered with your Total Gym, or start walking, swimming, cycling or doing other types of cardio and reading&nbsp;<a href="https://buffedd.com/" target="_blank">Buffedd</a>. If you're not counting calories and working to stay within a certain calorie goal, you won't see results as quickly, because your new muscles will be hidden by fat. In short, use the Total Gym for your strength workouts, but to see real results, also do cardio and eat a healthy diet. With that program, you should see very significant results and change your body within a few months.</p>	2018-01-13 22:13:00	46	02f28cbf-ef25-463f-9c77-901ae627ac54	\N	f	T
51	<p><strong>When Do Muscles Grow After Working Out With Weights?</strong></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-934546803caf7abee17c922abe804604.webp"></p><p>Lifting weights damages your muscles. Though that may sound strange, pumping iron at the gym actually depletes muscle-building nutrients in your body and creates microscopic tears in your muscle fibers. It is only after you workout that your body begins to repair the damage you inflicted on it and you experience the muscle growth you desire. Visit&nbsp;<a href="https://buffedd.com/" target="_blank">Buffedd&nbsp;</a>to know exactly how to build you muscles properly</p><p><strong>Overall Timeline</strong></p><p>Len Kravitz, Ph.D. of the University of New Mexico states that muscle growth occurs in your body when the breakdown of muscle protein is less than the rate of muscle protein synthesis. Every time you lift weights, you are breaking down the proteins in your body, but also increasing their rate of repair for a minimum of two to four hours. Heavy weight training stimulates your protein synthesis for up to 24 hours, thus maximizing your muscle growth until your next workout.</p><p><strong>Anabolic Phase</strong></p><p>In the book "Sport Nutrition for Health and Performance," Melinda Manore et. al. describe the anabolic phase as the 45-minute period after your workout when your muscles go into overdrive to repair the damage from weight lifting. While very little actual muscle growth takes place during this phase, it is critical to consume carbohydrates and proteins in a ratio of 3:1 to limit the amount of muscle damage present. A post-workout meal or supplement shake during this window stimulates hormone release and sets the stage for increased protein synthesis.</p><p><strong>Growth Phases</strong></p><p>A majority of the actual muscle growth in your body takes place during the rapid and sustained growth periods after the workout. The rapid growth period begins around the one-hour mark post-workout and lasts up to five hours after the workout. The sustained growth period is anywhere from five to 24 hours after you lift weights. Regularly consuming small meals and snacks of carbohydrates and proteins every two to three hours during the growth phases will help you keep your glycogen, amino acid and nitrogen levels elevated. All of these nutrients contribute to positive protein synthesis.</p><p><strong>Sleeping</strong></p><p>Doctors advocate sleeping for eight hours every night because this is a critical period for your body to recover and repair itself. Your body, however, runs out of muscle-building glycogen and protein while you sleep and enters a muscle breakdown stage called the catabolic state. In order to offset this breakdown, you should eat a dietary supplement or cottage cheese mixed with whey protein before bed. These products provide you with long-lasting fuel overnight to keep your muscle growth at its highest.</p>	2018-01-14 22:24:00	47	02f28cbf-ef25-463f-9c77-901ae627ac54	\N	f	T
52	<p>Diesel engines are commonly used in large vehicles for several reasons.</p><p>Larger vehicles are always&nbsp;<strong>heavier&nbsp;</strong>than small vehicles and they are commonly used to transport&nbsp;<strong>cargo or load.</strong></p><p><strong>Torque is the name of the game. High torque is needed to move heavy loads. If comparing a gasoline engine to a comparable diesel engine the diesel will always have higher torque. The higher torque comes from the need for a higher compressing ratio needed for compression ignition. To achieve the higher compression ratio a longer stroke is required. The longer stroke comes from a greater crankshaft offset. This offset gives greater torque.</strong></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-96cb58ec5a8d781d1cf567c6e3dda80b.webp"></p><p>Despite having only around 400 HP, this truck pulls 40 ton trailers with ease, due to its diesel engine鈥檚 huge torque.</p><p><strong>Another aspect is that diesels can make tremendous torque at very low RPM. Very simply put more fuel equals more torque when everything else is kept the same.</strong></p><p>A diesel does not have throttle plates and draws in the maximum amount of air on every stroke. In a diesel the amount of fuel added is what controls the power. The throttle controls how much fuel is added. This means that a diesel always runs lean. At idle the engine uses hardly any fuel. This lean mixture allows for the addition of large quantities of fuel even at low RPM.</p><p><strong>A gasoline engine on the other hand always has to keep the fuel mixture at optimal stoichiometric.</strong>&nbsp;This need to keep the mixture correct means that to get more fuel the engine needs to rev to higher RPMs. This means that a gasoline engine makes it's torque at much higher RPM than a diesel. This high end torque characteristic makes a gasoline engine hard to drive necessitating constantly keeping the RPM high.</p><p><strong>The only real draw back to this torque production is a limited RPM. This is compensated by a gear box with lots and lots of gears.</strong></p><p>If a gasoline engine was used it would have to be much larger. The much larger engine would make for greater fuel consumption.</p><p>Diesel has several other advantages which has attracted many large vehicle manufacturers such as -</p><ol><li>More efficient. When hauling a 40 ton trailer for very large distances, every drop of fuel matters.</li><li>More reliable in damp and wet weather due to absence of spark ignition.</li><li>Very durable. Diesels are built with stronger components to withstand higher stresses and compression ratios.</li></ol><p><img src="https://qph.fs.quoracdn.net/main-qimg-fd99e5f6bfb3f08a22799a328baa4834.webp"></p><p>(&nbsp;<em>This will probably outlive you, me, nuclear war, and the end of the Universe )</em></p><p>During the 40鈥檚 and 50鈥檚, gasoline trucks were very popular in the USA. However, they quickly switched to diesel due to complaints of too high fuel consumption, and due to gasoline鈥檚 less torque, the engine had to be revved pretty high to achieve speeds greater than 20 MPH. This further decimated fuel economy.</p><p>Due to answer being pretty popular, i鈥檝 decided to write down even more detail about diesels advantage in heavy vehicles.</p>	2018-01-10 22:27:00	48	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	\N	f	T
53	<p><strong>Life is like Tetris. If you fit in you disappear.</strong></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-7f26d76dc23531db1d25230f5bcfac07.webp"></p>	2018-01-02 22:31:00	49	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	\N	f	T
54	<p>Both are forced induction systems based on compressing air and introducing it to cylinders. Biggest difference is&nbsp;<strong>operating principle.</strong></p><p><strong>Turbocharger.</strong></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-b910ba06926ac6a26dc2706efddae2e4.webp"></p><p>Turbocharger is&nbsp;<strong>connected to car鈥檚 exhaust, whose kinetic energy spins the compressor.</strong></p><p>In naturally aspirated piston engines, intake gases are "pushed" into the engine by atmospheric pressure filling the volumetric void caused by the downward stroke of the piston (which creates a low-pressure area), similar to drawing liquid using a syringe. The amount of air actually inspired, compared with the theoretical amount if the engine could maintain atmospheric pressure, is called volumetric efficiency.</p><p>The objective of a turbocharger is to improve an engine's volumetric efficiency by increasing density of the intake gas (usually air) allowing more power per engine cycle.</p><p>The turbocharger's compressor draws in ambient air and compresses it before it enters into the intake manifold at increased pressure.</p><p><strong>This results in a greater mass of air entering the cylinders on each intake stroke. The power needed to spin the centrifugal compressor is derived from the kinetic energy of the engine's exhaust gases.</strong></p><p><strong>Supercharger</strong>.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-1de6dba19f8d70f7e67292ee275a7904.webp"></p><p><strong>Supercharger is connected to car鈥檚 crankshaft whose kinetic energy spins the compressor.</strong></p><p>A&nbsp;<strong>supercharger</strong>&nbsp;is an air compressor that increases the pressure or density of air supplied to an internal combustion engine. This gives each intake cycle of the engine more oxygen, letting it burn more fuel and do more work, thus increasing power.</p><p><strong>Now let鈥檚 see the pros and cons</strong></p><p><strong>Mechanically driven superchargers may absorb as much as a third of the total crankshaft power of the engine and are less efficient than turbochargers.&nbsp;</strong>However, in applications for which engine response and power are more important than other considerations, such as top-fuel dragsters and vehicles used in tractor pulling competitions, mechanically driven superchargers are very common.</p><p>The thermal efficiency, or fraction of the fuel/air energy that is converted to output power, is less with a mechanically driven supercharger than with a turbocharger, because turbochargers use energy from the exhaust gas that would normally be wasted. For this reason, both economy and the power of a turbocharged engine are usually better than with superchargers.</p><p><strong>Turbochargers suffer (to a greater or lesser extent) from so-called&nbsp;<em>turbo-spool</em>&nbsp;(turbo lag; more correctly, boost lag), in which initial acceleration from low RPM is limited by the lack of sufficient exhaust gas mass flow (pressure). Once engine RPM is sufficient to raise the turbine RPM into its designed operating range, there is a rapid increase in power, as higher turbo boost causes more exhaust gas production, which spins the turbo yet faster, leading to a belated "surge" of acceleration.</strong>&nbsp;This makes the maintenance of smoothly increasing RPM far harder with turbochargers than with engine-driven superchargers, which apply boost in direct proportion to the engine RPM.&nbsp;<strong>The main advantage of an engine with a mechanically driven supercharger is better throttle response, as well as the ability to reach full-boost pressure instantaneously.</strong>&nbsp;With the latest turbocharging technology and direct gasoline injection, throttle response on turbocharged cars is nearly as good as with mechanically powered superchargers, but the remaining lag time is still considered a major drawback, especially considering that the vast majority of mechanically driven superchargers are now driven off clutched pulleys, much like an air compressor.</p>	2017-12-26 22:33:00	50	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	\N	f	T
55	<p>We usually hear a phrase "What are we in the grand scheme of things!" That is mostly said to make us realize that we are just a small part of some "grand scheme" and our existence doesn't affect it much. But let me tell you, what we fail to realize is that, the "grand scheme" is actually&nbsp;<strong>US.&nbsp;</strong>That scheme is composed of us and the likes of us. So, you/we/us are the most crucial/important/essential part of the scheme. So next time whenever someone says this to you, try not to give a fuck! But also actively play your part because only we can affect the "grand scheme" and use it the way we want!</p>	2018-01-14 22:40:00	49	3af7e93a-7873-41fb-b991-43b89126f635	\N	f	T
56	<p><strong>You most definitely should learn how to write characters by hand.</strong>&nbsp;The very fact that you talk about "memorizing" characters shows that your approach to learning is not ideal.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-9ed1ca5185bfc8f1e4e5c5e808b6e92e.webp"></p><p>Writing words by hand (especially in cursive script) involves many more areas of the brain than typing. As a reasonably fast two-fingered typist, for example, typing the word "hand" only involves my left and right index fingers touching the appropriate capital letter shapes: H (left index on the shift key and right index on the H), A (left index), N (right index), and G (left index).</p><p>Typing is mindless tedium: LEFT, RIGHT, LEFT, RIGHT, LEFT, RIGHT, THUMB (for a space), LEFT, RIGHT, LEFT, LEFT, RIGHT, LEFT, RIGHT, 鈥?/p><p>When I write in cursive, however, each word is composed of a whole series of lines, loops and curves written on an invisible network of lines. Each letter must connect with the next at just the write angle and place, not too high and not too low.</p><p>In my brain, there is an individual mental map of each English word I choose to write. My mental maps for Chinese characters are even more complex and distinctive. This helps fix them in my mind much better than if I chose to type everything. Being able to write also helps me be a better reader.</p><p>There is experimental evidence of the relationship between writing and reading ability:</p><p><strong>(1)</strong>&nbsp;<a href="https://www.ncbi.nlm.nih.gov/pubmed/28131811" target="_blank">Activation of writing-specific brain regions when reading Chinese as a second language. Effects of training modality and transfer to novel characters. - PubMed - NCBI</a></p><p><strong>(2)&nbsp;</strong><a href="https://www.ncbi.nlm.nih.gov/pubmed/12880898/" target="_blank">Finger movements lighten neural loads in the recognition of ideographic characters.</a></p><p>If I type the character 鎵?nbsp;<em>sh菕u / shoou</em>&nbsp;"hand", I will need to hit five keys at most (S,H,O,U + a number to choose a character). Think about how much more processing power is involved when I choose to write the same character by hand. True, there are only four strokes, but consider the complexity:</p><p><strong>1st stroke:</strong>&nbsp;move right to left starting from the very top, slanting downwards and tapering off to a point.</p><p><strong>2nd stroke:</strong>&nbsp;(starting about 1/4 of the way down) move left to right slanting slightly upwards about as long as the first stroke. Be sure to remain parallel to the 1st stroke.</p><p><strong>3rd stroke:</strong>&nbsp;(starting about halfway down) again, left to right slanting upwards, parallel to the 2nd stroke, but significantly longer. This stroke should be almost the full width of an imaginary rectangle (tradition says a square, but in practice you are writing inside a standing rectangle).</p><p><strong>4th stroke:&nbsp;</strong>(starting just below the middle of the first line) top to bottom slightly curved line ending with a leftward hook pointing upward. This stroke is perpendicular to and goes through the middle of strokes #2 and #3.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-56d2d2be1b2ca931dba6756e314a0c44.webp"></p><p>Text which is typed is easily forgotten, but when you write something by hand, you are much more likely to remember it.</p><p>Whenever I go shopping, for example, I usually make a shopping list before leaving the house. Sometimes I forget the shopping list on the table, but in my mind鈥檚 eye I can still see the words I wrote:&nbsp;<em>eggs</em>&nbsp;was written in blue pen in the upper right hand corner, bananas at the bottom in thick magic marker.</p><p>If I simply entered the shopping list on my smartphone and forgot the smartphone, I doubt I could remember the list so accurately</p><p>This is still speculation, but some researchers wonder if a small part of the reason for the high IQs (especially the&nbsp;<strong>visual-spatial</strong>&nbsp;component) observed among Chinese people may have something to do with the structure of Chinese characters.</p><p><em>We assessed children from China, Russia, UK and two populations from Kyrgyzstan (Kyrgyz and Dungan), on arithmetic and other cognitive tests.&nbsp;</em><strong>As the Dungan population</strong><em>&nbsp;is ethnically similar to Chinese, speaks a form of Mandarin but&nbsp;</em><strong><em>uses Cyrillic (instead of character based Mandarin) as a writing system, we were able to test for the effect of the spoken language while controlling for its written aspect.&nbsp;</em></strong><em>Dungan children did not show any advantage in arithmetic over Kyrgyz children. This suggests that using oral Chinese, with its transparent number system and faster pronunciation of numbers, does not lead to mathematical advantage, at least for early arithmetic.&nbsp;</em>[<a href="https://www.ncbi.nlm.nih.gov/pmc/articles/PMC4374393/" target="_blank">Spatial complexity of character-based writing systems and arithmetic in primary school: a longitudinal study</a>]</p><p><br></p>	2018-01-17 22:46:00	51	74e14b79-514a-4db0-9de5-61f995daf9bf	\N	f	T
57	<p>The word "logical" shows that you have a misguided approach. Grammar can not be mastered by relying on what鈥檚 written in textbooks.</p><p>Grammar is how people use words: what are the different classes of words and how do they interact? This is best learned by wide exposure to language over a prolonged period (initially while paying attention to nuances):</p><p>I give my students the following:</p><p><strong>Three Techniques to Improve Your Grammar and Vocabulary:</strong></p><p>1. read</p><p>2.&nbsp;<em>read</em></p><p>3.&nbsp;<strong>READ</strong></p><p>After a while, the grammar will become part of your bones and you won鈥檛 need those boring grammar books and exercises.</p>	2018-01-16 22:49:00	52	74e14b79-514a-4db0-9de5-61f995daf9bf	\N	f	T
58	<p>If you are referring to a relationship between three countries, together making up the 鈥楪3,鈥?then in terms of the most powerful countries the&nbsp;<strong>US</strong>&nbsp;and&nbsp;<strong>China</strong>&nbsp;are the obvious ones. To make that 3 we need 1 more.</p><p><strong>Russia</strong>&nbsp;could be an option, this would mean we get a balance of world viewpoints, and it could help ease world conflicts that are influenced by Russia and the US eg. in Syria. But these days Russia鈥檚 influence is wearing off, they鈥檙e geographically big but not so much economically compared to others.</p><p>There could be another option. The&nbsp;<strong>European Union.&nbsp;</strong>Although not a country, the European Union is included in the G20, and has the GDP and size to be up there with the US and China.</p>	2017-12-20 23:08:00	53	cdd5a6a2-cb0d-409d-8cf8-95593d888423	\N	f	T
59	<p>New Zealand.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-1cf6e33206432a38bd5e25b91faf1341.webp"></p><p>I don鈥檛 like our flag. We had a referendum to change it, but we voted to keep the old one. The proposals weren鈥檛 any good anyway. But I still have some proposals of my own.</p><p>The biggest thing is, we need to get rid of the Union Jack, because we鈥檙e not part of the UK anymore, and it makes the flag too hard to distinguish from Australia鈥檚.</p><p>Here are some alterations.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-5d430e6b1bbd95d547a61807009fd141.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-5c1f8c72d2f9ce9c0e5d4ae135cc4e2d.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-aba14815d78ef13576cb6212fa43441f.webp"></p><p>And some more radical proposals:</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-e4f65e995d856a0680f28b8b07660a83.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-ef68a1da4ea087ad9de039378deb875a.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-ec9d8d4a2fbc008a8945ef74d7564c50.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-be55d2145ab0de4662db3fe42adfd319.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-d919b21157e5906a62a4b5bf77dd87ba.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-7b113a6502e80c74ebd29bc7eb487aa4.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-53ff9b8d94612b6ce1b22b5ba099c2d4.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-4877a2a0593ee8cefdfacc5473784642.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-40861932df96d9e107cddd418d0f9e0c.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-e8d6e932f3c6c33a202c633f2befe3e6.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-f739a6b5737ca7d005c14112e58b6ecb.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-752a20cdce52ddef5132aeaec2c6d121.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-52150ab1b4ca9e19a4580919b696cd2d.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-23661fa642c2af57fa1f8a5504349a9b.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-ba10c667ce7439ff97a1d6642849a224.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-6e7743ceedb39eaacdfa654f6623f971.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-b9a30eb7dcfe792f18fd473fefb3c727.webp"></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-1d6dc3b7eba911ed4a1fe8a4839dffef.webp"></p><p>A lot of them use triangle shapes, this represents New Zealand鈥檚 mountainous landscape. I also used imagery of ferns, korus (a Maori symbol of new life), two islands and white stripes, which reflects New Zealand鈥檚 Maori name Aotearoa meaning 鈥榣and of the long white cloud.鈥?And of course most of them contain black which is the national colour of NZ, and blue representing the ocean. White symbolises peace, as NZ is a very peaceful nation.</p><p>Please comment which one you like best. Also feel free to ask about the meaning of any of the flags, or give suggestions for a design.</p><p>EDIT: There has been a lot of support for the fern designs, so I have added some more designs that feature a fern</p>	2018-01-04 23:12:00	54	cdd5a6a2-cb0d-409d-8cf8-95593d888423	\N	f	T
60	<p><img src="https://qph.fs.quoracdn.net/main-qimg-4f43647829d0e9d055792c625f27bb20.webp"></p><p>I think I got better at being around people and increased my communication skills by consistently doing some things. Here are the most important.</p><p><strong>- Improv classes.</strong>&nbsp;By being in the same room with a bunch of strangers, I learned what could make them laugh by closely observing them and their reactions. All the exercises helped me think on my feet, feel prepared for the unexpected, and be able to laugh at myself. The classes also worked wonders on my overall communication skills. More on this topic&nbsp;<a href="https://www.hubgets.com/blog/improv-rules-effective-meetings/" target="_blank">here</a>.</p><p><strong>- Reading a lot.</strong>&nbsp;I like books and long format pieces that focus on the pain and burden of being a human being, pieces that people wouldn't normally be attracted to. By reading about how other people are dealing with deaths in family, sickness, uncertainty and doubt, I got to know more about what makes us, as a species, tick and break.</p><p><strong>- Traveling alone.</strong>&nbsp;The first time I went on a holiday alone, it was a means to clear my head and reconnect with myself. What I didn't realize at the point was that being all by myself also increased my communication skills and forced me to take lead and make decisions on my own in unfamiliar situations. It's an experience I'd recommend to anyone. It really tests your limits.</p><p><strong>- Learning from others' mistakes.</strong>&nbsp;By having deep, meaningful conversations with other people, I got to know intimate details about their lives and the mistakes they wished didn't make. So, when I got in similar situations,sometimes it was like I already had a tiny glimpse of my future, if I were to make a certain decision.</p><p><strong>- Putting myself out there, in various situations.</strong>&nbsp;I have friends with highly different backgrounds and I never said No to joining them to events that I thought were not necessarily my cup of tea. Most of the times, I was wrong about what my cup of tea was. I also like to keep being a beginner and go to workshops that have nothing to do with my line of work, but force me explore my creativity.</p><p>It was nice reading the other answers you got and I hope mine can help you too.</p>	2018-01-07 23:17:00	55	1a698814-ab5b-467e-b243-ece7a100e291	\N	f	T
61	<p>Here is the list of some mind blowing facts about Google.</p><p>Some of it ( For someone it may be all of it) are unknown and some may be repeated !!</p><ul><li>Every day, 16% of the searches that occur, are ones that&nbsp;<strong>Google has never seen before</strong>.</li><li>Google has found&nbsp;<strong>GPA's and test scores</strong>&nbsp;to be " <strong> worthless as criteria for hiring </strong> "; they have teams where&nbsp;<strong>14% of their employees haven't gone to college.</strong></li><li>If you search for " <strong> askew </strong> " in Google, the content will tilt slightly to the right.</li><li>The&nbsp;<strong>first Google Doodle</strong>&nbsp;was dedicated to the Burning Man festival attended by Google founders in 1998.</li><li>Google intends to scan all known existing 129 million unique books before 2020.</li><li>Microsoft pays you to use Bing instead of Google.&nbsp;<strong>(For some Region Only)</strong></li><li>The "I'm feeling lucky" button costs Google US$110 million per year, as it bypasses all ads.</li><li>Because Gmail first launched on&nbsp;<strong>April 1st of 2004</strong>, many people thought it was an April Fools' Day prank.</li><li>Google's First Computer Storage was Made From&nbsp;<strong>LEGO</strong>.</li><li>Google is developing a computer so smart it can program itself.</li><li>Google has a version of their site translated into the language of the&nbsp;<strong>Klingons</strong>, from Star Trek.</li></ul><p>Hope you Enjoyed !!</p><p>Some more will be Edited Soon !!</p><p>Enjoy ! :)</p>	2018-01-17 23:21:00	56	127317d1-c913-403f-ac63-dbfb4b6c0d3a	\N	f	T
62	<p>When the Google Pixel Buds are paired with a new handset, the Google Pixel 2, the earbuds can tap into Google Assistant, Google's artificially intelligent voice-activated product.</p><p><strong>The main part is that it can translate languages.</strong></p><p><img src="https://qph.fs.quoracdn.net/main-qimg-aa38babc0c86873620b18a4728ed1caa.webp"></p><p>In addition to the translation of 40 languages, Google Assistant can also alert users to notifications, send texts and give directions. The translation feature can be conjured by saying help me speak French</p><p>The controls, including swiping controls for volume, are built into the right earbud</p><p>During the demonstration, one employee, speaking Swedish, had Pixel Buds and the Pixel phone. When the phone was addressed in English, the earbuds translated the phrase into Swedish in her ear. The Swedish speaker then spoke back in Swedish through the earbuds by pressing on the right bud to summon Google Assistant. Google Assistant translated that Swedish reply back into an English phrase, which was played through the phone's speakers so the English speaker could hear.</p><p>Source: cnbc</p>	2018-01-11 23:24:00	56	ad3ffb5d-95b1-4528-a0a5-92591a21de34	\N	f	T
63	<p>Full Stack Developer is one of the new and trending career paths which has come into limelight and demand in past couple of years.</p><p><strong><em>But what is a Full Stack Developer?</em></strong></p><p>Putting in terms of&nbsp;<strong>Cricket</strong>, a&nbsp;<strong>Full Stack Developer</strong>&nbsp;is like a&nbsp;<strong>super All-Rounder</strong>who can&nbsp;<strong>bat well, bowl well, field well</strong>&nbsp;and even sometimes may take&nbsp;<strong>wicket-keeping duties</strong>&nbsp;if required.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-cf86e05530e81ba07169f5c7012ae601.webp"></p><p>Now similar goes in the technical world, a Full Stack Developer is proficient in all aspects of&nbsp;<strong>Development, Programming, Testing, Deployment,etc</strong>. Presently we are living in a world and time where optimization of resources is given a lot of impetus. So both companies as well candidates prefer to opt for Full Stack Developer role.</p><blockquote><a href="https://m.dailyhunt.in/news/india/english/deccan+herald-epaper-deccan/programmers+need+to+become+full+stack+players+says+niit+ceo-newsid-75073991" target="_blank"><strong>Programmers need to become full-stack players</strong></a></blockquote><p>Full stack developers is also cost-effective for both large-scale companies and startups as with a Full stack developer on board, they don't need to hire different specialists to work on a single project. Just have a look at the job trends of a full stack developer (According to&nbsp;<a href="http://indeed.com/" target="_blank">Indeed</a>)</p><p>In India, presently there is a huge demand for Full Stack Developers among all career paths. This can be seen in the huge spike in the hiring in this domain. In recent years one can see the total job-postings for Full Stack Developer grow exponentially to&nbsp;<strong>20000+ jobs</strong>&nbsp;available across the domain.</p><p><strong><em>What is required to become a Full Stack Developer?</em></strong></p><p>Full Stack Developers can work on any technology stack depending on the company and projects he/she is required to work on. An ideal Full Stack Developer knows each and every bit including Front end, Back end, Servers, Databases, Project management and Client coordination etc.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-e7b9ecea780413647657769af79dda40.webp"></p><p>MEAN stack is most in demand technology as well as best salaries are paid in the industry. So I would suggest you to acquire skills in MEAN Stack so that you may get your dream job in product-based company.</p><p>MEAN Stack is an evolving trend for the full stack JavaScript development. To be qualified as Full Stack developer one needs to be acquainted as well as become an expert in the following technologies:</p><ul><li><strong>MongoDB</strong>: It is the no SQL database . It uses JSON style documents for the data representation. The reason of choosing Mongo is that, it lets you make the use of just one language the whole way through.</li><li><strong>ExpressJS</strong>: It is a HTTP server framework for web applications that gives useful modules and components to work upon the common task for the website. It gives you the simple interface so that you can make request endpoints and cookie handling.</li><li><strong>AngularJS</strong>: It is a frontend JS framework to develop complex client side applications with modular code and data binding UI. It is used to develop the single page applications with the use of the MVC architecture; and maintained by Google. It improves the structure of the code and makes the testing easier with the dependency injection.</li><li><strong>Node.js</strong>: It is a concurrent JavaScript environment for building scalable and fast web applications. It compiles the JavaScript code to native machine code before the execution. It is lightweight and perfect for the real time applications.</li></ul><p>Average salary of a MEAN stack developer in INDIA is&nbsp;<strong>7 LPA</strong>&nbsp;and Average salary of a MEAN stack developer in US is&nbsp;<strong>$102000</strong>. Full stack developers are being hunted by companies like&nbsp;<strong>Amazon</strong>,&nbsp;<strong>Salesforce</strong>,&nbsp;<strong>Intel</strong>,&nbsp;<strong>Uber</strong>,&nbsp;<strong>Goldman Sachs&nbsp;</strong>and even growing B2C startups like&nbsp;<strong>Paytm</strong>&nbsp;and&nbsp;<strong>Flipkart</strong>. So, This is the best technology stack of present and coming years.</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-4f2e3ae4648c150e6b4b5589a9302afa.webp"></p><p>So in order to pursue a career as Full(MEAN) Stack Developer with better opportunities and growth,I would suggest you to:</p><ul><li>First&nbsp;<strong>acquire skills</strong>&nbsp;in MEAN Stack to get hired in product based companies as Full Stack Developer.</li></ul><p>Showcase your skill by&nbsp;<strong>doing some projects</strong>&nbsp;and portfolios so that companies can validate your skills and talent.</p>	2018-01-13 23:40:00	57	5bece49d-76a0-4184-88f7-9e420fec1666	2018-01-19 01:48:09.787	f	T
64	<ul><li><strong>The diversity is insane.&nbsp;</strong>There are a lot of immigrants here, especially in the big cities. 16% of the population is Hispanic and 13% is black. Besides this, even white people might be from many different places- It is estimated that 12% of the population is foreign born.</li><li><strong>Because of this, we are also a very divided country.&nbsp;</strong>A country so big is bound to have it's differences, but the US is extreme in this one. Because of the many different cultures, people tend to settle near others like them, thus creating racial tensions. You could drive for hours through the Midwest without seeing a non-white person, but there are also towns that are almost entirely Black/Hispanic.</li></ul><p><img src="https://qph.fs.quoracdn.net/main-qimg-26fcfd2fb2a7656316177ce7c24ecc6e.webp"></p><ul><li><strong>We have a teensy-little- very small obesity problem.</strong>&nbsp;Okay, it鈥檚 not small. It鈥檚 huge: two thirds of Americans are overweight/ obese, but I鈥檓 sure most people have heard that before.</li><li><strong>Guess what? We&nbsp;<em>also&nbsp;</em>have a teensy-little- very small body image problem!&nbsp;</strong>Did you know that 70% of Americans feel insecure about their weight?</li><li><strong>At the moment, we are the only country that hasn鈥檛 signed the Paris Climate Agreement.&nbsp;</strong>Just let that sink in鈥?/li><li><strong>Despite all of this, the United States is not as bad as some people make it out to be.&nbsp;</strong>The world (at least the western world) seems to have done a one eighty, from loving America to hating it.</li><li>Now some fun facts:&nbsp;<strong>100 acres of pizza are served in the US each day.&nbsp;</strong>It鈥檚 true, we really do love pizza.</li></ul><p><img src="https://qph.fs.quoracdn.net/main-qimg-bfa7dbfc054c054526d8c4c95a6f8a03.webp"></p><ul><li><strong>July 2nd is the real July 4th.&nbsp;</strong>July 4th was when the states signed the Declaration, not when they declared independence.</li><li><strong>10 out of 11 astronauts who walked on the moon were once boy scouts.&nbsp;</strong>If that鈥檚 not extremely American, I don鈥檛 know what is.</li><li><strong>Medical mistakes are the sixth most common cause of death.&nbsp;</strong>Okay, this one isn鈥檛 as much fun as the others.</li></ul><p><br></p>	2018-01-18 21:39:33.06	58	6f348000-2dbb-4b35-a8c3-c0a7f6266bb1	\N	f	T
65	<p>Non-Americans know nothing of American non-fast food dishes. They鈥檙e convinced that American food starts with McDonalds and ends with Coca-Cola.</p><p>They know nothing of:</p><ol><li>American desserts: Bananas Foster, Strawberry Short Cake, Pie (any kind of pie including Apple Pie a la mode), Key-Lime Pie, Pumpkin Pie, Pecan Pie etc.</li><li>American sandwiches: The Reuben, BLT, Cheese Steak, Pastrami, Heroes, Subs, Hoagies, Oven Grinders, Poor Boys, and Pulled-Pork Sandwiches</li><li>Barbecue (of all meats, seafood, vegetables, and even fruit!)</li><li>Tex-Mex</li><li>Great seafood (Catfish, Soft-shell crab, Clam Chowder)</li><li>Micro-brews 鈥?or how Happy-Hour works</li><li>Great salads - Cobb, 3 - Bean, Caesar etc鈥?/li><li>Thanksgiving dinner side dishes inspired by various heritages</li><li>Cajun and Creole dishes</li><li>Sourdough bread, Pumpernickel, Bagels, Bialys and various other American breads. They think that all Americans eat Wonder bread all the time.</li></ol><p><br></p>	2018-01-18 21:42:27.873	58	bed947db-669c-48b8-8205-5eacd083db4e	\N	f	T
66	<p>One big difference between the 60's and 70's was the fact that most of the major bands in the 70's performed on own their records, while in the 60's, studio musicians laid down the tracks for many of the big hits. There is a documentary on Netflix at the moment that details the history of "The Wrecking Crew", a group of LA studio musicians who created the rock sounds of the '60's.&nbsp;Some of them went on to become famous on their own, like Leon Russel.&nbsp;Another example is Duane Allman, who was a session musician at the Muscle Shoals studio and played guitar on many hit records before starting his own band.&nbsp;&nbsp;</p><p>The '70's were followed by the sterile, commercial sound of the '80's and MTV.&nbsp;The 80's were the beginning of the big sell out.&nbsp;Does anyone remember Neil Young singing, "Sample and Hold"?&nbsp;It's a terrible song in which he employed a vocal synth and other annoying special effects.&nbsp;It was beneath his talents as a folk/rock artist, but I guess even a rock star has to eat and pay bills.</p><p>&nbsp;The most noteworthy 70's bands were solid performers in an era before the coming digital technology of the 1980's began to mechanize the sound and music videos competed for our attention while distracting us from the music.&nbsp;In the '70's you actually had to know how to play your instruments and couldn't rely heavily on special effects and slick videos to enhance your performance. The net effect of the 1970's era was a more organic and honest sound.&nbsp;The original Allman Brothers Band is a benchmark of that sound.&nbsp;They didn't worry about their stage persona and they never appeared on TV, that I can recall.&nbsp;They just showed up for the gig and played like the devil.&nbsp;It was one hell of a performance!</p><p>A couple of places that sound can still be heard today is at Blues Festivals and Neil Young concerts.&nbsp;It's heartwarming to see that Mr. Young has returned to doing what he does best; turn the amp up to 11 and let it rip.&nbsp;The '70's are still alive every time he takes the stage. "Hey, hey, my, my...Rock and Roll will never die..."</p>	2018-01-18 22:02:50.457	59	462020ba-47b9-433a-9ca8-7b39bf92c2e3	2018-01-18 22:03:00.363	f	T
67	<p>Well, some Huawei smartphones are better than others, but the quality reflects the price point in most places. I had a Huawei 'Y' series lower end Huawei Y550, and although it was quite good, there were many slight problems and glitches, and Huawei cut corners with the strength of the glass covering the phone.</p><p>However, the higher end Mate series and P series are excellent, and very well designed, and so are the Huawei nexus devices, which I would recommend. So, overall I think Huawei phones are quite good, especially the ones over the $200 price point, as they are designed beautifully and preform like true high end devices despite their price point.</p><p>In the past few years, I think Huawei have really broken into the U.S market, especially with the nexus 6P, made by Huawei, which was an instant success.&nbsp;Huawei's new P9, which has a camera made by Leica, has already been met with interest, and the company is really showing that it can make high quality devices.</p><p>Hope this helps!</p>	2018-01-18 22:17:15.987	60	84d31cb3-c701-4962-a98f-3ce59af6e953	\N	f	T
68	<p>This is one area of photography that having some specialized equipment can make life much easier.</p><p>First, get a macro focusing rail.&nbsp;Even if you're using a true macro lens, getting the focus just right can be really tough.&nbsp;If you're tight on budget, get&nbsp;<a href="http://www.bhphotovideo.com/c/product/899257-REG/Dot_Line_dl_0322_Adjustable_Camera_Platforms_6.html" target="_blank">a cheap rail.&nbsp;</a>It will do 80% of what the expensive models will for about $20.</p><p>Second, a true macro lens (this is the expensive part) is hard to beat.&nbsp;I use the Canon 100mm f2.8 macro.&nbsp;In short, the optical quality has no peer; it really is that simple.</p><p>Less expensive optic kludges can work too, of course: reversed lenses, e.g., 50mm prime mounted backwards, extension tubes, even cheap diopters up front.&nbsp;They all have issues with handling, sharpness degradation, reduced light availability, and so on.&nbsp;However, go this route first if the budget is tight.</p><p>Third, lighting is always a challenge.&nbsp;Have access to and learn how to use strobes and light modifiers.&nbsp;Pick up a cheap ring light while you're at it.&nbsp;The least expensive ones can be had for under $50.</p><p>Things that move are a challenge.&nbsp;If you're shooting insects, pop'em into the freezer for a bit.&nbsp;They get sluggish.&nbsp;You get a small window of time for pics before they wake.</p><p>If you're shooting outside, there is&nbsp;<em>always&nbsp;</em>a breeze.&nbsp;Use strobes to shorten the exposure.&nbsp;Sometimes, just go with the flow:</p><p><img src="https://qph.fs.quoracdn.net/main-qimg-0f1d0a830c963eb74ffaa13f5d7e9825.webp"></p><p>Oh, and wear a hat.</p>	2018-01-18 22:50:57.82	61	da964209-eb9f-4c28-b4bc-22d9e0785d3f	2018-01-18 22:51:12.477	f	T
69	<p>Read photography books (and other sources like blog etc.) and&nbsp;<strong>practice</strong>,&nbsp;<strong>practice, and practice.</strong></p><p>Some books could be</p><p>1. Harold Davis' Creative Composition</p><p>2. David du Chemin's Within the Frame</p><p>3. David du Chemin's Photographically Speaking</p><p>4. Michael Freeman's The Photographer's Eye</p><p>5. Michael Freeman's The Photographer's Mind</p><p>6. Bert Krages' Photography the art of Composition</p><p>7. O'Brien and Sibley's The Photographic Eye (Great Book for Practice)</p><p>8. David Prakel's Basic Photography - Composition and Exposure</p><p>9. Michael Freeman's Perfect Exposure</p><p>A large part of photography is about composition and how to play with light. These books will give you a lot of new ways to look at both of them. These are not for fancy techniques like macro photography, off camera flash etc. but for training your eye about how to see composition and light in general.&nbsp;</p><p>Composition is a very important thing. It consists of</p><p>Lines and Curves</p><p>Points</p><p>Texture</p><p>Light and Shade (hard light)</p><p>Shape</p><p>Space</p><p>Balance</p><p>Motion and Blur</p><p>Perspective</p><p>Color</p><p>Learning how they act alone and in combination with each other, and how they behave together is very crucial in developing the photographic skills. That will be useful for all types of photos, both fancy and non-fancy. These will also give you a lot of inspirations.</p><p>I really enjoyed reading these photography books and learned a lot from these. I am not advertising them, just sharing the experience of my photographic journey.</p>	2018-01-19 01:12:57.377	61	d669f1fa-544a-47e6-86fc-6d5ecfd07645	\N	f	T
71	<h2>Web development is easy. Full stack isn鈥檛.</h2><p>Entering the&nbsp;<a href="http://usersnap.com/blog/get-started-web-development-8-elementary-tips/" target="_blank">web development industry is a relatively easy task today</a>, but riding the information technology wave as a&nbsp;<a href="https://usersnap.com/jobs/full-stack-developer?gat=fullstackblogpost" target="_blank">full stack developer</a>&nbsp;is not everyone鈥檚 cup of tea.</p><blockquote><em>In fact, many even call it a myth!</em></blockquote><p>In an ever dynamic work environment, with too many new technologies being released too quickly, the task does sound rather daunting, if not impossible.</p><p>Being a full-stack developer requires you to know about all the front-end technologies and all the back-end technologies.</p><p>It would demand you to know all programming languages. Or at least the most used ones.</p><p>You need to be good at working with everything, ranging from databases to user interfaces and the stuff in between. But is that even possible? Is it unrealistic to expect someone to be good at everything? Will the client still need different experts for that one project?</p><p>And here are some tips and tricks to get you there 鈥?at the ultimate destination of&nbsp;<strong>being a full-stack developer</strong>.</p><h2>Tip 1: Find your niche.</h2><p>Most pursuits for success begin with not knowing where you want to be.</p><p>True. You may argue, full stack is about learning it all.</p><p>But that鈥檚 where you鈥檙e mistaken. Full stack means identifying where you want to excel and then creating the path that gets you there.</p><h3><strong><em>First: identify business and customer needs.</em></strong></h3><p>And second: Decide your area of focus.</p><p>Web technologies 鈥?such as&nbsp;<a href="http://usersnap.com/blog/tracking-js-the-computer-vision-power-of-javascript/" target="_blank">JavaScript</a>&nbsp;鈥?for example, are today a lucrative path for many. If you too relate to this area, then your full stack would, for example, consist of HTML, CSS, JavaScript, general-purpose programming languages, database systems, web server, deployment operating systems, payment systems and a version-control system. This on its own will get you a long way.</p><h2>Tip 2: One language, not all.</h2><p>With your focus in place, you next need to zoom in on the technologies you鈥檙e your stack will need.</p><p>Here too, most developers try to learn all that they can. Doing that is not only overwhelming&nbsp;but given the kind of technology range available out there, it鈥檚 close to impossible.</p><p>What you need to do then, is to focus on learning a few important technologies. For example, you needn鈥檛 worry if you don鈥檛 know all the general-purpose programming languages. You can choose to learn either Python, Ruby, PHP or the others.</p><p>Make sure to check out&nbsp;<a href="https://www.codementor.io/programming/tutorial/beginner-programming-language-job-salary-community" target="_blank">this great article about the best programming languages you can learn in 2015</a>.</p><h2>Tip 3: Iron out the kinks</h2><p>Now, this is something we鈥檝e been learning since the days we went to school. When you鈥檙e clear on your fundamentals all else becomes easy.</p><p><strong>Full stack developers</strong>&nbsp;need to be comfortable with both the backend and front end of software development. The base of most web development is JavaScript &amp; HTML/CSS, so at a beginner level, strengthen your knowledge on these.</p><p>To know how good you are, test your skills by creating basic pages.</p><p>JavaScript, though a full programming language in itself, is as important, with 99% of all web based applications using JavaScript in some form or the other.</p><p>Your knowledge about servers, networks, hosting environments, algorithms, data structures, programming languages, and databases should be absolutely clear&nbsp;before you move up your stack.</p><p>And most importantly, you should be able to create the link between each piece of the puzzle.</p><h3><strong><em>It鈥檚 not about knowing single pieces of information, it鈥檚 about connecting the dots.</em></strong></h3><h2>Tip 4: Jack of all trades, king of one</h2><p>As you expand your knowledge, you will quickly learn that it鈥檚 difficult to gain expertise in all that you do.</p><h3><strong><em>Don鈥檛 give up. In fact, this is where most developers fail, trying to master too much.</em></strong></h3><p>What you need to do is become comfortable with working with all the important technologies.</p><p>That comfort level will improve with practical experience. And while you鈥檙e doing that, make one technology your main focus. This will provide you with an edge over your competitors and allow you to deliver some amazing results. Choose wisely, though, your specialty should align with what your clients need most.</p><h2>Tip 5: Taking a Course</h2><p>Even if you鈥檝e prepared properly, you鈥檒l need to be ready to learn new things on demand.</p><p>Basically, you need to be prepared to learn anything and everything that comes your way. To do this, consider taking up an online course.</p><p>There are many organizations that offer them. The&nbsp;<a href="http://www.theodinproject.com/" target="_blank">Odin Project</a>&nbsp;is one good resource.&nbsp;<a href="https://teamtreehouse.com/" target="_blank">Treehouse</a>,&nbsp;<a href="https://www.codecademy.com/" target="_blank">Codecademy</a>&nbsp;are other great resources as well.</p><p>Steer clear from courses that are too expensive and promise to take you from zero to the top in a matter of hours!</p><p>Opt for courses that have a good mentoring background, suit your schedule and come with coding projects and one on one or group-based tutoring sessions that allow you to learn quickly and get hands on experience as well.</p><h2>Tip 6: Building a network</h2><p>The best way to stay relevant in the market is to stay up to date. Heck, it鈥檚 the only way. For this, build a network of peers online.</p><p>There are many options to join a community. Social media sites, online forums, chat rooms and groups offer a great way to do so.</p><p>I especially recommend looking at various slack groups, like&nbsp;<a href="https://slashrocket.io/" target="_blank">slashrocket</a>&nbsp;鈥?a community of developers.</p><p>Further Slack groups can be found&nbsp;<a href="http://www.slacklist.info/" target="_blank">here</a>&nbsp;or&nbsp;<a href="http://www.chitchats.co/" target="_blank">here</a>.</p><p><a href="https://ds6br8f5qp1u2.cloudfront.net/blog/wp-content/uploads/2015/09/how-become-fullstack-developer-slack.png?x88475" target="_blank"><img src="https://ds6br8f5qp1u2.cloudfront.net/blog/wp-content/uploads/2015/09/how-become-fullstack-developer-slack.png?x88475"></a></p><p>These networks link you up with people sharing similar interests. Moreover, such platforms give you instant access to different ways of thinking and advice too.</p><h2>Tip 7: Make something on your own</h2><p>It鈥檚 finally all about creating a good quality product, an easy to use end-user experience and great functionality.</p><p>As a full stack expert, the diversions with so much technology can be tempting. But it is important&nbsp;<a href="http://usersnap.com/blog/how-we-implement-new-product-features-backed-by-customer-feedback/" target="_blank">not to lose focus on what your end product should look like</a>.</p><p>You must be able to see, feel and experience your end product. You must be able to build it securely. It must serve the purpose it was conceptualized for. And when you have such focus, working towards it becomes much easier and much faster.</p><p>Build something on your own. Learn about building something with a specific purpose in mind. In any case, it can always function as something you can show off.</p><h2>Wrapping it up.</h2><p>The path to becoming a full stack developer will take time.&nbsp;It鈥檚 not only about learning various front-end and backend technologies. It鈥檚 also about understanding those two areas in more detail and making communication between those two areas easy and smooth. So you need to have plenty of patience to get there. You also need to be eager to learn, and there is a lot to learn.</p><p>So if you鈥檙e the kind of person who enjoys reading&nbsp;<a href="http://usersnap.com/blog/12-best-web-development-blogs-reading-right-now/" target="_blank">software &amp; web development blogs</a>&nbsp;(Well, you鈥檙e here for one!) becoming a full stack developer is an easier process.&nbsp;The dynamics of the market are ever changing, and you never know what technology you鈥檒l be implementing next.</p><p>So be persistent and don鈥檛 lose focus. They say that the fastest way to learn something is to enjoy it. Have fun with what you do and you鈥檒l get there.</p>	2018-01-19 01:54:26.497	57	661781b1-4755-488b-8d2b-34ac1bb42ae2	\N	f	T
77	<p><strong>hello  ddd</strong></p><p><br></p><p><strong><span class="ql-cursor">锘?/span></strong></p>	2020-12-05 09:31:19.587	57	434decd7-89d9-422d-853c-22d3eca887d1	2020-12-05 09:31:47.863	f	T
\.


--
-- Data for Name: AspNetRoleClaims; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetRoleClaims" ("Id", "ClaimType", "ClaimValue", "RoleId", "TRIAL307") FROM stdin;
\.


--
-- Data for Name: AspNetRoles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp", "TRIAL311") FROM stdin;
1	Admin	\N	\N	T
\.


--
-- Data for Name: AspNetUserClaims; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUserClaims" ("Id", "UserId", "ClaimType", "ClaimValue", "TRIAL314") FROM stdin;
\.


--
-- Data for Name: AspNetUserLogins; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUserLogins" ("LoginProvider", "ProviderKey", "UserId", "ProviderDisplayName", "TRIAL317") FROM stdin;
\.


--
-- Data for Name: AspNetUserRoles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUserRoles" ("UserId", "RoleId", "TRIAL320") FROM stdin;
e8deb3b8-da18-4ab7-ad8f-812e65aa6fb8	1	T
\.


--
-- Data for Name: AspNetUserTokens; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUserTokens" ("UserId", "LoginProvider", "Name", "Value", "TRIAL327") FROM stdin;
\.


--
-- Data for Name: AspNetUsers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUsers" ("Email", "EmailConfirmed", "PasswordHash", "SecurityStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEndDateUtc", "LockoutEnabled", "AccessFailedCount", "UserName", "Id", "FirstName", "LastName", "Intro", "Gender", "Location", "DefaultIconNumber", "ConcurrencyStamp", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "TRIAL324") FROM stdin;
MarkBalaba@iknow.com	f	ACyoyqqPCRKMipexzBVQUItl/pq3xyWbTk3hCqeFMH2AvmPctqyC2pRwsv4P1iHP8Q==	058486b1-4b54-423f-b3c1-2b22fb8ee4ce	\N	f	f	\N	f	0	markbalaba0	02f28cbf-ef25-463f-9c77-901ae627ac54	Mark	Balaba	Bachelor Physical Fitness	1	Cagayan De Oro, Philippines	7	\N	\N	\N	\N	T
testuser1@zz.zzz123	f	AQAAAAEAACcQAAAAEK6OONQjIy/yWzYr7r3PJMqW79K7PUaXmMlQa7ec3xdx5F7MsMZBvrQdhwnPePMqqA==	7KKFDQWUKXY6B7JTKZFHV4DMWXGFBDXB	\N	f	f	\N	t	0	liefuzhang5	0429d42c-1b2f-4183-806b-7e63bd7d0087	Liefu	Zhang	\N	0	\N	3	e8d268c1-8441-417e-92de-9bab4fd18aca	\N	TESTUSER1@ZZ.ZZZ123	LIEFUZHANG5	T
testuser1@zz.zzz	f	AQAAAAEAACcQAAAAEDmXw1D7kmy0ieNEm8raZ2GFsJ9EzQcavmsF+fl69ArhhIVUx2byzV6SUIc/U4fhVw==	6LQ5AR7CKBSHESA5UM7WD3V7U5XWRXEB	\N	f	f	\N	t	0	liefuzhang10	0a3342ba-1bb3-4855-a2b0-8149d131932b	Liefu	Zhang1	\N	0	\N	1	bf7dcab7-0c7c-46a7-be3d-b140b2498be0	\N	TESTUSER1@ZZ.ZZZ	LIEFUZHANG10	T
MitulGedeeya@iknow.com	f	APFYRPcgkuVq0qtIL0QdJPc1IZ+7oOK3agJ7FlYcUzlRIpnHVMyRW5ngGvv7/VLa4g==	71fba719-9ea0-4b1e-8ff7-6983965b8476	\N	f	f	\N	f	0	mitulgedeeya0	127317d1-c913-403f-ac63-dbfb4b6c0d3a	Mitul	Gedeeya	Reader , Thinker and Implementer !!	1	Surat, Gujarat, India	0	\N	\N	\N	\N	T
NicoleBryant@iknow.com	f	AJb2dZUJQGgvMfgAI1NUCv8eech/uP6zSsdo8PrIaAPeKzpRhekAh5rZEdOuRT6paw==	0a42b5df-3bdb-4fb5-9d75-49d696c5719c	\N	f	f	\N	f	0	nicolebryant0	174b4461-5b84-40da-bba1-71a45bf0d6a3	Nicole	Bryant	Lives in New Zealand	1	\N	4	\N	\N	\N	\N	T
AlexandraBi@iknow.com	f	ABeuR7OqQMkkLWQpMc4GeE9UVPTGLc1+nUxWIxTsVpbIANQXqG6qwAAq/sWh28gJJQ==	24b8adbf-8c5b-4d09-8575-7fec8d017d01	\N	f	f	\N	f	0	alexandrabi0	1a698814-ab5b-467e-b243-ece7a100e291	Alexandra	Bi	Communication specialist	2	\N	5	\N	\N	\N	\N	T
testuser1@zz.zzzz	f	AQAAAAEAACcQAAAAEKXYibqk9ThdAsf+xFr18pNA7oSIpE0qvlo5yg9VkGJh8C8f/7X/IiN+7M8xghBffQ==	LSKEFRLZGA7DUJYOYDNHRDHL7OYTTCUH	\N	f	f	\N	t	0	testuser11	373a8dda-f678-42ba-afea-0b083645f97c	test	user1	\N	0	\N	3	fded08cd-dd9a-4a66-896f-d882fceab95e	\N	TESTUSER1@ZZ.ZZZZ	TESTUSER11	T
MeetBhatt@iknow.com	f	AG9qkKwl+RBVzhGhNrpdaH5clmisGiTPFG/a3CN+LvAQK6u26SjxK8CKX0Ht5MzETg==	349e399e-2284-4d27-aa49-f25beeef2e4c	\N	f	f	\N	f	0	meetbhatt0	3af7e93a-7873-41fb-b991-43b89126f635	Meet	Bhatt	Mechanical Engineer. Student of Life鈥?	1	Gujarat, India	3	\N	\N	\N	\N	T
testuser1@zz.zzd	f	AQAAAAEAACcQAAAAEKIuGwl+DH2vvs/Otiugfr6l/aWzzubJOplQRFbzh/JSILvXVBrgkjHdR2QAUwDtbw==	4S36BWZHAL23AMTXD6OQTCHFVKOSBCOL	\N	f	f	\N	t	0	liefuzhang6	434decd7-89d9-422d-853c-22d3eca887d1	Liefu	Zhang	\N	0	\N	1	e33cb332-7dbc-4726-865d-9f04199ec675	\N	TESTUSER1@ZZ.ZZD	LIEFUZHANG6	T
AlTaylor@iknow.com	f	AEXOLmXdeot3Kw3Y/vZ6ogync1QX0CyXv2pEfEPOMrmaMQW6FqDNUS9U/pqxnlWBRg==	87001736-76a5-4c8d-b0eb-443a1ec0834c	\N	f	f	\N	f	0	altaylor0	462020ba-47b9-433a-9ca8-7b39bf92c2e3	Al	Taylor	I've twiddled my thumbs in a dozen odd bands.	0	Lived in 3rd Stone from the Sun	8	\N	\N	\N	\N	T
FJShi@iknow.com	f	AHE7A7XnG4mqBAf2UcmVNT/wlK8meJrVzmf13LaVOz5A+xXUiPa2bNrq7gUFRuDJVQ==	d979596e-ccfc-46b0-a1d9-fd4eaf74f792	\N	f	f	\N	f	0	fjshi0	4f3518e8-abf7-457f-b6ab-15e37e99b81b	FJ	Shi	Lives in Shanghai	1	Shanghai	3	\N	\N	\N	\N	T
liefuzhang@1163.com	f	AQAAAAEAACcQAAAAEJqfR6mWMv1hmwL2ZJiW7U+ALS39d8Do7n3yZfqbiCr9c8tTcsz+tWUBn+7GQJzVtQ==	377OJT7LCT2OXV3CBE7653ZCVEA4BND3	\N	f	f	\N	t	0	liefuzhang2	50db9270-98f6-4f9c-8f12-4864cd923bd7	Liefu	Zhang	\N	0	\N	5	b1f131c6-8f5c-410e-9043-d6bcbea11431	\N	LIEFUZHANG@1163.COM	LIEFUZHANG2	T
KarunaRastogi@iknow.com	f	AFVEMbwqXeIoPcvY4Vrh5q9TItI370vKl4YPYIv+lQtmDoQuji/O33+/H/qcnOcHxg==	caa7301a-b4a5-4f7c-aa9c-260a6c77d229	\N	f	f	\N	f	0	karunarastogi0	5bece49d-76a0-4184-88f7-9e420fec1666	Karuna	Rastogi	Frontend Developer (2015-present)	1	Bengaluru, Karnataka, India	1	\N	\N	\N	\N	T
DavidWyatt@iknow.com	f	AJcxG+3GHY3DaplmR5u7Q86ABJUqb5ZUjABzcE3vyouMTlqc7SA23qcwqocmPcmEiw==	445448ef-4027-4215-aae9-66936adb3168	\N	f	f	\N	f	0	davidwyatt0	5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9	David	Wyatt	I am a very chilled out and relaxed kind of guy that loves to laugh.	1	Auckland	7	\N	\N	\N	\N	T
ThomasPeham@iknow.com	f	ABu3Tgv9rdk/NB67WEcYv8cjoeYBm2F2FN63JFBCWeRtFC5dk7rhhhYxm9zs4p4I/A==	71238376-a232-449d-8907-0689cabccf4b	\N	f	f	\N	f	0	thomaspeham0	661781b1-4755-488b-8d2b-34ac1bb42ae2	Thomas	Peham	\N	0	\N	1	\N	\N	\N	\N	T
ShashankGupta@iknow.com	f	ALY3d3Ko4+dPa2dTyo7LcyK/jT0yqaldqFP/mdMlTh4ccd7rCS8Plg3Bqio+9hB1aA==	0c3eac48-b585-447d-bb86-8703d207f509	\N	f	f	\N	f	0	shashankgupta0	6d47028c-a645-4ab9-a28f-a372bd74da0b	Shashank	Gupta	\N	0	\N	3	\N	\N	\N	\N	T
RoseLindberg@iknow.com	f	AJ5Rs51QXLsGHKaXX37cGhNbsC4/8U55npyoD8ZCdQLbm7v+YYIrbgl6pNzMW7l/5A==	6abcee3a-c889-4a7e-9d3d-265781bb69b5	\N	f	f	\N	f	0	roselindberg0	6f348000-2dbb-4b35-a8c3-c0a7f6266bb1	Rose	Lindberg	I learn stuff.	0	Lives in The World	8	\N	\N	\N	\N	T
RobertMatthews@iknow.com	f	AAC1zd+9WGs7aOKrIxuMh0oooDmVnBXYHhyjt6wWxYRA9iAwO44P5Mm0UB3BQRP/VQ==	5f8c3235-daa3-4df1-9c7e-67269f63c017	\N	f	f	\N	f	0	robertmatthews0	74e14b79-514a-4db0-9de5-61f995daf9bf	Robert	Matthews	Multilingual thinker who should do more and think less	1	\N	5	\N	\N	\N	\N	T
TimTran@iknow.com	f	AFvkdGbvik1WhHr8H9XOkR25VIe3S6mGGzM+2JNCgdd6Mgkp+k+m29+xEZRw1BDZbQ==	f5f412de-7707-42ed-9ad2-a330f7035eb4	\N	f	f	\N	f	0	timtran0	75245e73-98c1-4828-a7b7-77c0eefa3d7d	Tim	Tran	Studied at Brooklyn Technical High School	1	Ho Chi Minh City	5	\N	\N	\N	\N	T
test123@123.ddd1	f	AQAAAAEAACcQAAAAECr7PHk8/wCfWOkqCNvrb7xcu+Jl5wgk7L/vwTMOSvRrlaFq9k7ih9cB3rt3Zaagog==	YACIQGOHSBRO55YYEYRSW3KGT6RCI7AO	\N	f	f	\N	t	0	test123@123.ddd1	7b7257c9-73ec-44d8-9190-7630ddd3873e	test123@123.ddd1	test123@123.ddd1	\N	0	\N	5	8654a388-5152-435a-96d9-27000a75632a	\N	TEST123@123.DDD1	TEST123@123.DDD1	T
BrenanNiranjan@iknow.com	f	AET2Rxgvg7OqyXxv+sO07KpSxyofCI4N7cXSMJRKycyamfCAAVdlzKn8JdrwdVi3HA==	bd0318b2-e5b7-470f-b1fe-0d3fac9fdef0	\N	f	f	\N	f	0	brenanniranjan0	84d31cb3-c701-4962-a98f-3ce59af6e953	Brenan	Niranjan	Literally obsessed with smartphones	1	Stratford-upon-Avon, Warwickshire	0	\N	\N	\N	\N	T
testuser1@zz.zzz	f	AQAAAAEAACcQAAAAENkVH1vosIFEORRf6aA2240OqoFNV+E0PRsU1oPw1xCGqVIyTMH0P2iuSIs3j+oAcQ==	IWSNXFC47O4EQUXV57KXAZPF5F32IKYE	\N	f	f	\N	t	0	testuser10	8cff4f74-0c67-4c6a-9fc3-9ce3ff2c12eb	test	user1	\N	0	\N	0	200d5a28-6c69-4e5e-bc76-d5034368500f	\N	TESTUSER1@ZZ.ZZZ	TESTUSER10	T
RandallJacobs@iknow.com	f	ANDHcSAmrhrnfR+YBrJWcsjGEuhAllyZi8887jWzkhbcMlQ8OJ4PcrN1SO8xWu3NXw==	4c60ef1f-9265-41a3-ad67-e195bb929d03	\N	f	f	\N	f	0	randalljacobs0	929981dc-d19d-42df-8e7e-f6896239ff54	Randall	Jacobs	I am a Libertarian, with 70 years of experience	1	Friendswood, TX	6	\N	\N	\N	\N	T
JasonSlavin@iknow.com	f	AH/AgUtAmg9ujyeZAvGGF1vO6EXVW7U4K+zPctoamoeTMpJ8uSx0hywJC7BJCegkfQ==	cae21731-56c6-41da-944c-120028224015	\N	f	f	\N	f	0	jasonslavin0	aa32eb82-d698-468e-8c0b-dc3a6bebfc12	Jason	Slavin	Front End Developer (2017-present)	1	\N	2	\N	\N	\N	\N	T
NVSubbaraju@iknow.com	f	AEgNecKsudap7EFbESfGrm/RUEUcIhbabgIlHeHxtDbHIGl68ee2r7JlcxrFLylmZg==	690681d3-e8b7-4fdb-94db-abe0c7596686	\N	f	f	\N	f	0	nvsubbaraju0	ad3ffb5d-95b1-4528-a0a5-92591a21de34	NV	Subbaraju	\N	0	\N	6	\N	\N	\N	\N	T
DideCollins@iknow.com	f	AMteBPEOcOQ6AyAUcMhR8Ea/EXi9EdWPVOpMGBh4cvQ6LLEle+EsYlZtBr4lTYsHag==	fac297f1-77f6-4103-a41c-541be8a1b32b	\N	f	f	\N	f	0	didecollins0	b6072fcd-7589-46f6-841a-d3c2adb8323d	Dide	Collins	Hospital/Theatre Administrative Assistant (1987-present)	2	Melbourne, Victoria, Australia	3	\N	\N	\N	\N	T
testuser1@zz.zzz111	f	AQAAAAEAACcQAAAAEH43kCAt3cQ4+ztsJioJteRfTPnTcyj6D5NLywY2CCMYUqjcXbuDBqw9fkxRK0Bp/g==	5TKTPQVT5W7D2Q3HN7VOE6QZCQHZQMZV	\N	f	f	\N	t	0	liefuzhang4	b83e5d67-7164-48de-ad81-7399e89fb60d	Liefu	Zhang	\N	0	\N	7	67a85cae-fdbe-4b99-ba47-0d801dbe0769	\N	TESTUSER1@ZZ.ZZZ111	LIEFUZHANG4	T
testuser11@zz.zzz	f	AQAAAAEAACcQAAAAEPziuOT9+mYqox2gE5ykCTh5AIwZcXmDfgXKs/suvL0dPNn7FgaeCCT//DXfSF2S1w==	JVNW6SQDWAQAE2ZI3GZOZP5MDCHC4YEC	\N	f	f	\N	t	0	liefuzhang3	b872a336-f216-48ea-81b9-eff3520fa759	Liefu	Zhang	\N	0	\N	3	1b8030f6-c516-40dc-83f5-41ff459d8adc	\N	TESTUSER11@ZZ.ZZZ	LIEFUZHANG3	T
MosesRap@iknow.com	f	AMhqOIPo3Q7mri+OIxBr0XrG0h/VjXGh/uS53QoTip6p1lmEyKdTh++a5+ITDj7VTw==	f8fc0d22-6d98-4981-9546-4516b6942781	\N	f	f	\N	f	0	mosesrap0	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	Moses	Rap	Former Fisherman. Studied at Cook	1	Germany	3	\N	\N	\N	\N	T
MarcelEnglish@iknow.com	f	AOX/jFwe3ehfazseGQRqrMS8vsZ8oKSEmoSYiB5fuyBWOadixMyysy6R/+DEr67IKQ==	2ee2518a-a5c5-47ec-9279-42b5371a09d0	\N	f	f	\N	f	0	marcelenglish0	bed947db-669c-48b8-8205-5eacd083db4e	Marcel	English	EFL Teacher living in Europe	0	Europe	0	\N	\N	\N	\N	T
xulina1211@gmail.com	f	AB23xj41vxMfyyFX2zjTTkwpnQvJafMJcTUTSV8WLsdSQJXV6giEi72X1sg36w2qUQ==	d168659e-24e5-497f-8b03-ffc6bd310bed	\N	f	f	\N	f	0	linaxu0	c9082805-5404-423c-a84e-768974d55a9a	LINA	XU	\N	0	\N	1	\N	\N	\N	\N	T
JacobGreig@iknow.com	f	AEhwhJE/j/IbPO1r01udM3spmEGbYiFEL13wY54Cwr93Ewi9vG7Oa30NINZ2ilWPTg==	7742bf99-2a5c-4214-b142-f460f01e8036	\N	f	f	\N	f	0	jacobgreig0	cdd5a6a2-cb0d-409d-8cf8-95593d888423	Jacob	Greig	Lives in New Zealand	1	New Zealand	9	\N	\N	\N	\N	T
RodneyHoward@iknow.com	f	AEe1fSU4lkih9QmPX5kaWsGeK0OcECzjhdfuVmvXiFht3S5ifDNCENoU8Aifqow+eA==	46e36090-48e1-4c15-ae9c-afdf39993375	\N	f	f	\N	f	0	rodneyhoward0	cef53707-04ea-45d5-b7ac-c86f87aa625d	Rodney	Howard	Systems Administrator	1	Lives in Arlington, TX	7	\N	\N	\N	\N	T
BrianRegan@iknow.com	f	AFZB/T3ISgcRci7CaXxMiUZEwjQOdehkIzOOQW/h+Aq/uspC6w2C9raG4i8Xl9QCwg==	fb444819-6af0-46ed-996e-0880e76cef3f	\N	f	f	\N	f	0	brianregan0	d29c1e65-8eff-4459-bbce-a5f39714e011	Brian	Regan	Data Science Masters Student	1	Galway, Ireland	8	\N	\N	\N	\N	T
KrishanuKarmakar@iknow.com	f	AJ6A3Ic7w28uSdg5GXPH/mSlezgwm3fqZ8ELrW/rSskP7LRHmLvS4VH9KRw6LlmQ8w==	82b495c3-2e8e-461e-ba5c-4e475e92986c	\N	f	f	\N	f	0	krishanukarmakar0	d669f1fa-544a-47e6-86fc-6d5ecfd07645	Krishanu	Karmakar	Gourmand, Researcher of Fiscal Policy, Arm chair bound social reformer.	0	\N	1	\N	\N	\N	\N	T
RobertLee@iknow.com	f	ABTPwuP1/9HJlXTUTOFG4YZzMAhzhyCx4hZiJKamyvMClIO3t5l40T+5YInRFZFwtg==	0416b662-b88d-461d-a134-3bfedf0e904f	\N	f	f	\N	f	0	robertlee0	da964209-eb9f-4c28-b4bc-22d9e0785d3f	Robert	Lee	Well, that was fun...	1	Lives in Earth	4	\N	\N	\N	\N	T
AbhinavRajendran@iknow.com	f	AGdmAHKW/W689LO+m5C7htxA0LH0D9Fa0e/k0VwJh916XXTYb1VzeRqCjBv7PU+PxQ==	444798b1-7e44-40c0-99e8-ab09f4f200d0	\N	f	f	\N	f	0	abhinavrajendran0	e40ee71a-e3de-4b33-a877-0cc91fdcb337	Abhinav	Rajendran	\N	1	\N	9	\N	\N	\N	\N	T
liefuzhang@163.com	f	ALpIw9lBhZXL78UFAryzsT7zCWwTwwasJ/uzpq7tsNvN2zImO0gszYbpgsNYhUx6Ug==	5429ae79-7c1b-4321-a3fa-fbca9f16d45d	\N	f	f	\N	f	0	liefuzhang1	e8deb3b8-da18-4ab7-ad8f-812e65aa6fb8	Liefu	Zhang	\N	0	\N	0	\N	\N	\N	\N	T
BenKolber@iknow.com	f	AF3b36JA+NPqIgKlCZZGYnR1pjsHrMVreM0KyGrMynh1CyT/IHVNQJ9y14RJ362Szg==	3d6d607f-9aea-4873-b010-5ddcaddfa652	\N	f	f	\N	f	0	benkolber0	eb5d625e-aa8f-4709-a296-f92b1df0dd3e	Ben	Kolber	Former paratrooper, now I cook and code.	1	\N	2	\N	\N	\N	\N	T
liefuzhangnz@gmail.com	f	AAlZr6AuXwUOMauJ8hYRNAla6Mj+vrkkTsb+EY+OK4KKvyjckN3ydYP38qvQdp4MZw==	b7e023a7-e172-4e37-86b5-974123f51283	\N	f	f	\N	f	0	liefuzhang0	fe8c5539-5945-4cfb-9892-6cb4d3935c90	Liefu	Zhang	\N	1	\N	4	\N	\N	\N	\N	T
\.


--
-- Data for Name: Comments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Comments" ("Id", "Content", "CreatedDate", "AnswerId", "AppUserId", "ReplyToCommentId", "TRIAL327") FROM stdin;
1	I'm using huawei cellphone and it's working as well as I ever expected	2018-09-16 01:12:20.54	67	e8deb3b8-da18-4ab7-ad8f-812e65aa6fb8	\N	T
2	hello	2018-10-27 08:03:13.01	60	fe8c5539-5945-4cfb-9892-6cb4d3935c90	\N	T
\.


--
-- Data for Name: Questions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Questions" ("Id", "Title", "Description", "AppUserId", "IsDeleted", "TRIAL330") FROM stdin;
18	What are the signs that you are a skilled software developer?	To be a developer is easy, but to be a skilled developer is hard. What characteristics does a skilled developer have?	fe8c5539-5945-4cfb-9892-6cb4d3935c90	f	T
19	How can full stack developers learn so many skills?	Full-Stack Web Development, according to the Stack Overflow 2016 Developer Survey, is the most popular developer occupation today.	fe8c5539-5945-4cfb-9892-6cb4d3935c90	f	T
21	What are some of the best ways to learn programming?	\N	aa32eb82-d698-468e-8c0b-dc3a6bebfc12	f	T
22	What鈥檚 one inaccuracy in a science fiction movie that drives you crazy?	\N	fe8c5539-5945-4cfb-9892-6cb4d3935c90	f	T
36	How has life in New Zealand changed in the years since 2000?	Anything from personal life to public changes and anything in between	5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9	f	T
37	What things happened to you in New Zealand that made you want to visit there again?	Anything that's memorable, laughable, fun or simply something you just enjoy doing	5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9	f	T
38	What is the level of China's Internet technology?	You're welcome to share your knowledge on this matter, no matter it's from personal experience or from other reliable source	4f3518e8-abf7-457f-b6ab-15e37e99b81b	f	T
39	Do Chinese, Korean, and Japanese people have the same traditional festivals?	I've heard that there are some festivals that are celebrated in those three countries. To what extent they are celebrating the same festivals and are they celebrated in the same way?	4f3518e8-abf7-457f-b6ab-15e37e99b81b	f	T
40	Why is 127.0.0.1 used for localhost? Does anyone know why that number was chosen?	\N	75245e73-98c1-4828-a7b7-77c0eefa3d7d	f	T
41	How do you resolve communication issue between two servers?	\N	cef53707-04ea-45d5-b7ac-c86f87aa625d	f	T
42	Why do most people consider robots as threats? Aren't robots just machines?	\N	d29c1e65-8eff-4459-bbce-a5f39714e011	f	T
43	Why is machine learning regarded as the best career?	\N	d29c1e65-8eff-4459-bbce-a5f39714e011	f	T
44	What is the best movie out in November 2017?	\N	d29c1e65-8eff-4459-bbce-a5f39714e011	f	T
45	Why does China rank first as a top visited country in Asia despite the negative views of foreigners?	\N	929981dc-d19d-42df-8e7e-f6896239ff54	f	T
46	How many days of workouts at the gym are required to see visible changes in the body?	\N	02f28cbf-ef25-463f-9c77-901ae627ac54	f	T
47	How long after a workout does the body continue to build muscle?	Can anyone explain the concepts of musculoskeletal injuries, minor fiber breakdown and etc.?	02f28cbf-ef25-463f-9c77-901ae627ac54	f	T
48	Why do they use diesel in large vehicles rather than petrol?	\N	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	f	T
49	Do you have a philosophy of life?	If yes, what鈥檚 your best piece of advice for living?	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	f	T
50	What is the difference between Turbocharged engines and Supercharged engines?	\N	bd3c0d34-47aa-4eb0-b003-f137b139b3f8	f	T
51	Should I bother learning how to write Chinese characters by hand?	I can already speak, read and type.	74e14b79-514a-4db0-9de5-61f995daf9bf	f	T
52	How can I study English grammar logically if I am a Chinese?	\N	74e14b79-514a-4db0-9de5-61f995daf9bf	f	T
53	Who should be the third country in G3 if there is such a group?	\N	cdd5a6a2-cb0d-409d-8cf8-95593d888423	f	T
54	Do you dislike your own country's flag design?	If so, what would you change about it?	cdd5a6a2-cb0d-409d-8cf8-95593d888423	f	T
55	How did you acquire intelligent interpersonal communication skills?	\N	1a698814-ab5b-467e-b243-ece7a100e291	f	T
56	What are some mind-blowing facts about Google?	\N	127317d1-c913-403f-ac63-dbfb4b6c0d3a	f	T
57	What are the facts a full stack developer must know?	\N	5bece49d-76a0-4184-88f7-9e420fec1666	f	T
58	What are the top 10 things non-Americans don't know about the USA?	\N	6f348000-2dbb-4b35-a8c3-c0a7f6266bb1	f	T
59	Why is American music from the 1970's so good?	\N	462020ba-47b9-433a-9ca8-7b39bf92c2e3	f	T
60	Are Huawei smart phones good or bad?	Can you give some reviews about how reliable Huawei phones are based on your personal experience?	84d31cb3-c701-4962-a98f-3ce59af6e953	f	T
61	How can one become better at photography?	Especially in the area of Macro photography	da964209-eb9f-4c28-b4bc-22d9e0785d3f	f	T
\.


--
-- Data for Name: TopicFollowings; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."TopicFollowings" ("UserId", "TopicId", "TRIAL333") FROM stdin;
e8deb3b8-da18-4ab7-ad8f-812e65aa6fb8	31	T
\.


--
-- Data for Name: TopicQuestions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."TopicQuestions" ("QuestionId", "TopicId", "TRIAL337") FROM stdin;
18	13	T
19	13	T
21	13	T
40	13	T
41	13	T
43	13	T
57	13	T
36	14	T
37	14	T
54	14	T
38	15	T
40	15	T
41	15	T
56	15	T
57	15	T
42	16	T
43	16	T
39	17	T
40	17	T
37	18	T
45	18	T
19	19	T
38	19	T
40	19	T
41	19	T
56	19	T
57	19	T
46	20	T
47	20	T
48	21	T
50	21	T
37	22	T
45	22	T
48	22	T
46	23	T
47	23	T
42	24	T
48	24	T
51	25	T
52	25	T
61	25	T
36	26	T
37	26	T
39	26	T
46	26	T
47	26	T
49	26	T
58	26	T
39	27	T
53	27	T
37	28	T
38	28	T
39	28	T
45	28	T
53	28	T
54	28	T
58	28	T
55	29	T
56	30	T
60	31	T
38	32	T
39	32	T
45	32	T
51	32	T
52	32	T
53	32	T
19	33	T
57	33	T
46	34	T
53	35	T
58	35	T
59	35	T
56	36	T
60	36	T
22	37	T
44	37	T
59	38	T
42	39	T
49	39	T
60	41	T
19	43	T
21	43	T
57	43	T
61	44	T
\.


--
-- Data for Name: TopicUsers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."TopicUsers" ("UserId", "TopicId", "TRIAL340") FROM stdin;
\.


--
-- Data for Name: Topics; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Topics" ("Id", "Name", "Description", "TRIAL340") FROM stdin;
13	Software Engineering	Software engineering is the process of analyzing user needs and designing, constructing, and testing end user applications that will satisfy these needs through the use of software programming languages. It is the application of engineering principles to software development. \r\n\r\nIn contrast to simple programming, software engineering is used for larger and more complex software systems, which are used as critical systems for businesses and organizations.	T
14	New Zealand	New Zealand is an island country located in the southwestern Pacific Ocean comprising two main landmasses (the North Island and the South Island) and numerous smaller islands. \r\n\r\nThe geography of New Zealand is highly varied, from snowcapped mountains to lowland plains. The government system is a parliamentary democracy and a Commonwealth realm; the chief of state is the queen of the United Kingdom, and the head of government is the prime minister. New Zealand has a free market economy in which the prices of goods and services are determined in a free price system.	T
15	Internet	The Internet is the global system of interconnected computer networks that use the Internet protocol suite (TCP/IP) to link devices worldwide. It is a network of networks that consists of private, public, academic, business, and government networks of local to global scope, linked by a broad array of electronic, wireless, and optical networking technologies.	T
16	Artificial Intelligence	Artificial intelligence is a science and technology based on disciplines such as Computer Science, Biology, Psychology, Linguistics, Mathematics, and Engineering. A major thrust of AI is in the development of computer functions associated with human intelligence, such as reasoning, learning, and problem solving.	T
17	History	The study of past events, particularly in human affairs.	T
18	Travel	Travel is the movement of people between relatively distant geographical locations, and can involve travel by foot, bicycle, automobile, train, boat, bus, airplane, or other means, with or without luggage, and can be one way or round trip. Travel can also include relatively short stays between successive movements.	T
19	Web Development	Web development is a broad term for the work involved in developing a web site for the Internet (World Wide Web) or an intranet (a private network).	T
20	Health	Health is the level of functional and metabolic efficiency of a living organism. In humans it is the ability of individuals or communities to adapt and self-manage when facing physical, mental, psychological and social changes with environment.	T
21	Automobile	An automobile (or a car) is a wheeled motor vehicle used for transportation. Most definitions of car say they run primarily on roads, seat one to eight people, have four tires, and mainly transport people rather than goods.	T
22	Environment	Environment is everything that is around us. It can be living or nonliving things. It includes physical, chemical and other natural forces. Living things live in their environment. They constantly interact with it and adapt themselves to conditions in their environment.	T
23	Sport	Sport includes all forms of competitive physical activity or games which, through casual or organised participation, aim to use, maintain or improve physical ability and skills while providing enjoyment to participants, and in some cases, entertainment for spectators.	T
24	Economics	Economics is the social science that studies the production, distribution, and consumption of goods and services. Economics focuses on the behaviour and interactions of economic agents and how economies work.	T
25	Education	Education is the process of facilitating learning, or the acquisition of knowledge, skills, values, beliefs, and habits. Educational methods include storytelling, discussion, teaching, training, and directed research.	T
26	Lifestyle	Lifestyle is the interests, opinions, behaviours, and behavioural orientations of an individual, group, or culture.	T
27	International Relations	International relations (IR) or international affairs, depending on academic institution, is either a field of political science, an interdisciplinary academic field similar to global studies, or an entirely independent academic discipline in which students take a variety of internationally focused courses in social science and humanities disciplines.	T
28	Country	A country is a region that is identified as a distinct national entity in political geography. A country may be an independent sovereign state or one that is occupied by another state, as a non-sovereign or formerly sovereign political division, or a geographic region associated with sets of previously independent or differently associated people with distinct political characteristics.	T
29	Interpersonal Communication	Interpersonal communication is an exchange of information between two or more people. It is also an area of study and research that seeks to understand how humans use verbal and nonverbal cues to accomplish a number of personal and relational goals.	T
30	Google	Google is an American multinational technology company that specializes in Internet-related services and products. These include online advertising technologies, search, cloud computing, software, and hardware.	T
31	Huawei	Huawei Technologies Co., Ltd. is a Chinese multinational networking and telecommunications equipment and services company headquartered in Shenzhen, Guangdong. It is the largest telecommunications equipment manufacturer in the world, having overtaken Ericsson in 2012.	T
32	China	China is a country in East Asia bordering the East China Sea, Korea Bay, and the South China Sea. Neighboring countries include 14 sovereign states. The terrain is diverse in China with mostly mountains along with deserts in the west and plains in the east.	T
33	Full Stack Developer	The industry definition of a Full Stack Developer is an engineer who can work on different levels of an application stack. The term stack refers to the combination of components and tools that make up the application. The components could be in the front-end or the back-end of the system.\r\n\r\nThe main objective of full stack engineer is to keep every part of the system running smoothly. A Full Stack Developer can performs tasks ranging from resizing an image or text in a webpage to patching the kernel.	T
34	Food	Food is any substance[1] consumed to provide nutritional support for an organism. It is usually of plant or animal origin, and contains essential nutrients, such as carbohydrates, fats, proteins, vitamins, or minerals. The substance is ingested by an organism and assimilated by the organism's cells to provide energy, maintain life, or stimulate growth.	T
35	America	The U.S. is a country of 50 states covering a vast swath of North America, with Alaska in the northwest and Hawaii extending the nation隆炉s presence into the Pacific Ocean. Major Atlantic Coast cities are New York, a global finance and culture center, and capital Washington, DC. Midwestern metropolis Chicago is known for influential architecture and on the west coast, Los Angeles' Hollywood is famed for filmmaking.	T
36	Business	A business (also known as an enterprise, a company, or a firm) is an organizational entity and legal entity made up of an association of people, be they natural, legal, or a mixture of both who share a common purpose and unite in order to focus their various talents and organize their collectively available skills or resources to achieve specific declared goals and are involved in the provision of goods and services to consumers. A business can also be described as an organisation that provides goods and services for human needs.	T
37	Movie	Movies, also known as films, are a type of visual communication which uses moving pictures and sound to tell stories or inform (help people to learn). \r\n\r\nPeople in every part of the world watch movies as a type of entertainment, a way to have fun. For some people, fun movies can mean movies that make them laugh, while for others it can mean movies that make them cry, or feel afraid.	T
38	Music	Music is a form of art; an expression of emotions through harmonic frequencies. Music is also a form of entertainment that puts sounds together in a way that people like, find interesting or dance to. Most music includes people singing with their voices or playing musical instruments, such as the piano, guitar, drums or violin.	T
39	Philosophy	Philosophy is a way of thinking about the world, the universe, and society. It works by asking very basic questions about the nature of human thought, the nature of the universe, and the connections between them. The ideas in philosophy are often general and abstract. But this does not mean that philosophy is not about the real world. Ethics, for example, asks about the ideas underlying our everyday lives. Metaphysics asks about how the world works and of what it is made.	T
40	Physics	Physics is the natural science that involves the study of matter and its motion and behavior through space and time, along with related concepts such as energy and force.	T
41	Mobile Phone	A mobile phone, known as a cell phone in North America, is a portable telephone that can make and receive calls over a radio frequency link while the user is moving within a telephone service area.	T
42	Earth	Earth is the planet we live on. It is the third planet from the sun. It is the only planet known to have life on it. Lots of scientists think the earth formed around 4.5 billion years ago. It is one of four rocky planets on the inside of the Solar System. The other three are Mercury, Venus and Mars.	T
43	Programming Language	A programming language is a type of written language that tells computers what to do in order to work. Programming languages are used to make all the computer programs and computer software. A programming language is like a set of instructions that the computer follows to do something.	T
44	Photography	Photography is a way of making a picture using a camera. A person who makes pictures using a camera is called a photographer. A picture made using a camera is called a photograph or photo.	T
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion", "TRIAL291") FROM stdin;
20201204041312_InitNetCore	3.1.0	T
\.


--
-- Data for Name: __MigrationHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__MigrationHistory" ("MigrationId", "ContextKey", "Model", "ProductVersion", "TRIAL294") FROM stdin;
201712251102544_InitialModels	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5adb6ee336107d2fd07f10f4d41659cbc9628136b077913a49116c6e8db38bbe2d1889768852a44a52591b45bfac0ffda4fe4287ba5294644b76eca68b222f09c539331c1e0ec9c3fcfde75fa3778b903a4f5848c2d9d83d1c0c5d07339f0784cdc76eac66afbe77dfbdfdfaabd159102e9c8f79bfd7ba1f583239761f958a8e3d4ffa8f38447210125f70c9676ae0f3d04301f78e86c31fbcc3430f03840b588e33ba8b9922214efe803f279cf9385231a2573cc05466edf0659aa03ad728c432423e1ebbe43de39f5de7841204cea798ce5c0731c6155210daf10789a74a70369f46d080e8fd32c2d06f86a8c459c8c765f7aed10f8f74f45e699843f9b1543cec0978f83a4b87679b6f9454b7481724ec0c12ab967ad449d2c6eecf319629b4edec784285ee98e57490e67e901b1c3849f34131eb400efd73e04c62aa6281c70cc74a207ae0dcc60f94f8eff1f29eff8ad998c5949a414158f0add2004db7824758a8e51d9e65a15e04aee355ed3cdbb030336cd2415c30f5fac875aec1397aa0b8987363c053c505fe09332c90c2c12d520a0ba6317092b59a77cbd73d5114e7ee8065b0465ce70a2d2e319babc7b10bbfbace3959e0206fc942f8c0082c29305222c6ebbc9c62e90b12a573b6635fb05ac4fa0c5a18d7e889cc938436a0b9ce1da6c947f948a274810ef4874f39af24c42d7878c76966527ef9748fc41c2b8887377e9ef258f8564023afa4fcca859046d77111e8ceff2f00dbd7259f13a67f5d41cca3376f3a11736396193cda866a39975aa89633b10ff7a169467485688babd2c98accf8d61c9bd9a129ba5e0ba188a2c77ac86cbe9465b19aea7718d1354c7f9e127c16224277eee5f6912bfe41ecde11645bf09d7b811216e86abe633797dc47bbd98737dc43bb1690c67db4a9c27429202752729f24f118411a45b63ab4331638ab2b6e9acb7c50904ea81b24824a01cee10ee2da15e1869d628a15764efcf48c3d41d247413dab3080a063304586ca60ca937235a0ef6a7ea03461a1b74c44e1ea22a1d811a6ea758c309f4488aecc8665d5b1fee9b116f8f697531ce9e5c1d4ca9177719c1f0febce0b1fd604accbccc833d8d4816426e157ce6ce3f6b98e68f6bcaec06ce04b6531ae83de9c320d23db17691a12b0b9eb67a74c5aadc04681051699f3e470a21bf142351c6f6048d9094766a5d9a680069d62553f6d96d5b156306a3caa82e83c3601a4acec609c4d421b46c1420bca485c15cf3c211b9d9acfd0f64cae2df0c5088a91d7d8b0b62e1b1846ac76bdaf0eb0ebe02b296b197e6be1e9567afaa7a0a9d85828250b3ae721dfd38b55524a6b5eaaade51a9cd722c28dae5014c1a9c710e5b216679a2a729357d3feba55986278be6c90af8a680b4f706f45736c7d05d710e93911529d22851e903e774d82b0d6ad52135a565beeca5ef6f569cb17606ea17f376a4f5916eab532333987c184bad626f7f106aad74d132d1451241aee39134ee390b5d7de76eb4cd13201b2a6ee1815bdca44aa7ce88e971f3a4ca8b68388e6ad95d3dabe539bb0da99b1ca804efc4817757f6e2415bf3f2f9acd76c30943e431418ce617350b4551dc6c325a6a7cc73969b5decdd494a2848951b67647ca540713266bea8e516a0a264cd9da1d29130d2a89499bba63e492800992b7f5217f7ee3af723f6fdd33f56bdbb7dda5f05e6ce3d6763dcab6cef50f6bb5bd34ede23a90a22712e87d74ba940a8703dd6130fd8d4e2881f1961dae102333d8c052b5cf3d1a1e1e590f752fe7d1cc9332a01d5fcef62e59129dd5b55a7d4f45b3f27ac59e90f01f91f826448b6f4da80d5fa8b6c2abbe422583afdddb2e60252fc6eeef89c9b173f1cba7d4eac0b91140bd6367e8fcb12625fd5e8abe8c49afbdd8e413557fb379bedc350b36fb4a6107eaf4a2cdfab781adc85fd1ffb742b235feadc02a3afe564855ad7e2b285b8fef01b61371fbdfd2b24be169bff2f53e84c715b7e7ffb240bdb51a5d0856dd14e8e160f08c22f4be14e7f6dbd80b5399ebc253371979a58a9c1eb8c76ef0a02b6f5ac9dac5cf269db855636e826e142657c9cfebd4e73627adbae53ef4e99a8aba528ab5e7a9aacabd48fdb9df001ba6b3a666ec445eaedf4361511aff060af540927909a1ff299461bfb21c8b3e176cc6f38a60459477b18e0e5758a100d6ea895064867c059f7d2c65f29cff11d13839893de0e082ddc42a8a150c19870fb4f22f51babaacf29f68e8d5984737c935493ec710204c0243c037ecc798d0a088fbbce1b8de02a1cb5676bbd073a9f42d63be2c90ae39eb0894a5afa8b6f7388c2880c91b36454f7893d8808197788efc652e27b483ac9f886ada47a704cd050a658651dac39fc0e1205cbcfd0702a795860d2d0000	6.2.0-61023	T
201712252305033_AddMoreModelsAndRelationships	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed1ddb6edb38f67d80fd07414f3b838ceda428b01bd833c83aed2068d374e374b06f0523d18e3012e591a84c82c17ed93eec27ed2f2c298912c58b445ab2ec04458122a1c8c373e739e439edfffef3dff9cf4f51e83cc2240d62b4704f2733d781c88bfd006d166e86d73ffecdfdf9a7bf7c377fe7474fceaf6cde1b3a8fac44e9c27dc0787b3e9da6de038c403a89022f89d3788d275e1c4d811f4fcf66b3bf4f4f4fa7908070092cc799df66080711cc7f21bf2e63e4c12dce40781dfb304ccb71f2659543753e8108a65be0c1851b7c40f11fae731106806cbe82e1da750042310698a076fe25852b9cc468b3da920110de3d6f2199b706610a4b94cfebe9a6d8cfce28f6d37a2103e565298e234b80a76f4a764cc5e53b31d5add84518f68e30163f53aa73a62ddc0b94fe0113d711b73a5f86099d56727452707e524c3f71f2c1934ae24431e89f13679985384be002c10c27203c713e67f761e07d80cf77f16f102d5016863c420425f2ad3140863e27f11626f8f916ae4b34af7cd79936d74dc585d5326e4d41c215c26fce5ce713d91cdc87b0923747ee0ac709fc052298000cfdcf006398200a03e61c937617f6222a8ac94cb621d1316221ae730d9e3e42b4c10f0bf7ed6c468ce27df0047d365462f10505c4a2c82a9c64508165c7ce09a4185f92bfd8eef4e73b623fd6b0fe99c19472a39b73ed70889525d6303e81c760930b438395ebdcc2309f903e04dbc2b827ece3d7423353c2e2248e6ee3905bc8be7dbd03c9065221c59a09ab384b3c0bd428a14ab4e807154afcb8844ee3a30a95f9b4b6df56abae596668d76cc137cb16f7ba0b70085becfaeceddb7d98f5254cbd24d81642d46e7e6aea54c6b4d74aedfb9a2b3301adb9320332c5ec2ede065e07626c8e02afe2931eadf2bb2d56ed4e844197dd48f545ed48eacfbd5c494e94b11fc9677f7322e25ef4efd6d8e0e5b990cee3bab79d694f6bc10e6dec4c8d546e2f3242dcb0dac45a1031b6afc2fa0dcd8b4efe665de25e1fe34d803a4c6ca8637aa753cf281a148f1565a8388c111a1f2c4a9ce4736780f3d7c00495c8f43873c9d03aa0b19d0e9dc6240127ee9b1a317e820a3b2b07516161e127ca35afc55db4bb805b08c2831cb2ef221084e3a7079f1f621c7f49da76263f0e901a107124f1de7721bedfa7c760ef6d2cd9f831f640477cb423813b46fba62e47198ea87c9289cbb948d3d80b727c84e3a33ab49ac4bd43bed395b315fce4433ec256e271822df13104091279baa22fb941973084183a175e715bba04a9077c99bb8410df18a58a57354aecd6b389d00fd23ec4a9c1840621205c1261113719202c7bc00079c116841d3c11d619fa4e4a6db583f8e5126ea9e520dc41bbc9d6fce59f8c42b59320882e0ecda79c76b52b5d234ad249571d32d5922dcc4b543451ae1a78636b8a8a9811b44445b3c9b6ec9ae870da51c7acadf25404b03d354471d7d2ede086d41289a4b1f444a2fc65684a994ab40a55cc2bda754496a61a9c4241ca3bb45678b604f291422b5aca4ca5a73928439126cc2a8ae902bdbb4d28281bcb2a140cd87deb116c42bc76eb0c9ef4b661eeedf46087b69122aea58fb08445302989ce13dffc65f6092b526722c3327b4ecb205e449f025d412cdef0d451b4102448f4370170372e12889aab1d40d81d8904a16460c7f2f212545a5db80183c5a5d6eb6054662f80e2042733a4be1be3a6e95e8c44f33148462a3a1a32900cd12087e02055388bb9499354033634af076516e803e3eed09843b8147d0bd9ca60787f2473b2d010ad89f74c223e7bc2e518cf446f76259ed9b18672959fee8c60ec6916fc310780e1370cb50dd7a021591bd198c534f6c4aba218014aeded7af3417af96af1759dd2d79dd3bb7bba013581dd3355e771f56d3e2d2af6ca81f95453da37bf06db6d80365ca95f39e2ac8a3abfe58f2bfb6ab8a880316db2568c1eaa9d709c800d14bed25a2f1fbe0f92145f020cee01bd0b5cfa9134ad117d68ce55b65533c090a5c50e5a369ffeccc5382c009143d072c17b42484443d8fca54df2e8f24287d6568210248a7bfa651c6611d207b4fad555951c0fa21ab480c3d7bc3560f11fcce1f1575f3cb8b62b313d3496fcf290740931b5014146527a20895fba0b6d6a9391aeb59dac5dda56c5aaf6faa65fba1f8d2babb77800e590398c461d050fa9f1e195e987f604ea528e220db1d70ccdbafda845f154c8af2f46f6af140712671111d94b334f0bed85a95eb61f5972d51f3c106ef8a8a4504594bb094313201bca44bb7a3fa2a95fe57918f5a839a4f2999d07530e99c3a8dfcc7930f5a839a4f251bcc19862c81c067bf2e681b0311be5670fd84ddd67a3e3ab7e332b90f5bf4a7f54da5d7d5429aff284a0a98ce27c15f222990b46daae7fc6e076b543487b596c84500ec40e23314fb3169a94b7eac241adf0d80453016a92dbf1786684553f49b6bfb6ef2e4c29f716a754f65fe5e042ae3d2ff3deee5e3b29112ea6b80ec1fd31f06912bc7a4e318c2674c264f57bb80c833cd56313ae010ad6841145c1997b363b3d137af78ea78f6e9aa67ea8b837909be99a221ba1662ea03ced2ca2b56d2c6bb6b4a14790780f20f96b049ebe1fa24bcd273fe361bad472faa5f79d2b72983e2ddc3ff365e7ced5bfbed62b4f9c9b84e8dfb93373feddb3bdcd74f36295c5c6f6dd5eaf43f11a1d574cede4a2cafedd104ad8453f440ddcbe7feaf00aa138685fac36f055bd4a81bd9d1d5017ecda3c5e8744a4768b1d6d74a70af8c3b0d0c09a7b9e296201fb50aadea84f1fcc998ae5e7dae0c0bac4bc17a4661979bf88452c0db740cc4ab177f6d5dae4549eaa4981bacd60a4834d1103308ced762e97ed33c8da595efa3cd4a616bb5b66a3b1eee091f91efb190ed9c0c0aac9c6ee5718a37852fb50fd92fb117ad5d38e2d6dcd8bce21257df86a71b3be825e72b6f22c2f4cd6561ee4f0d26eb956b697f85e8e803a3014113029933e7675d13ee61cb7bea82efc3999c9f252caea55698cb91447559996279fb174c6b43da7772f4e55556bd67f339b4c066cc1194bf4faaa87e3edb1e93a5f8e21d56826b52a44f6e93dc64c3c5adfa08fd08998bc361fcde17358351aed10b2d7a1917358751b9c5caf2eca51d5e7d6d2e6563cf32f5cff9e5edb16d733bad6205d0f5c6b0b9c6a037d038eb2434edf20a702ae697150f5bf697be75470958d286d6d755d5d75ba4db47d2aaabd740ce2be699964469160f65a5177eda617fa98fd84cab69ad6ae9aa609f19d1647d52d28b53b1d96a8e1fa01ed08b39273bf7ebfaa51abb5ed4f53f1d68b48c9f085baca61c9646d64ed64aa4be8e4b0466a1338024287ed68b497a570604825de83372cb6a8ae59c1e6209e5579b228aa4c87275fafd26695a13dd57a78c22dda34e5925012a572ff49030995d3605383a0ff6503825e233eade65ca175cc4265012336457857bc8618f82478bd4870b0061e269f3d98a6f93fd5f62b083348eb1ceea17f856e32bccd30211946f761e31f0ea5e176dbfe792f6a13e7f94d5e22940e41024133a0158f37e81f5910fa15deef15cf9a1a10348e2fcb78a82c312de7d93c57903ec5c81050c9be2afdb883d13624c0d21bb4028f7017dc88e7f90837c07b6695bd7a20dd8268b27d7e19804d02a2b48451af27bf121df6a3a79ffe0fc99b0a11ab640000	6.2.0-61023	T
201712260708060_AddSomeTopicsToTopicsTable	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed1ddb6edb38f67d80fd07414f3b838ceda428b01bd833c83aed2068d374e374b06f0523d18e3012e591a84c82c17ed93eec27ed2f2c298912c58b445ab2ec04458122a1c8c373e739e439edfffef3dff9cf4f51e83cc2240d62b4704f2733d781c88bfd006d166e86d73ffecdfdf9a7bf7c377fe7474fceaf6cde1b3a8fac44e9c27dc0787b3e9da6de038c403a89022f89d3788d275e1c4d811f4fcf66b3bf4f4f4fa7908070092cc799df66080711cc7f21bf2e63e4c12dce40781dfb304ccb71f2659543753e8108a65be0c1851b7c40f11fae731106806cbe82e1da750042310698a076fe25852b9cc468b3da920110de3d6f2199b706610a4b94cfebe9a6d8cfce28f6d37a2103e565298e234b80a76f4a764cc5e53b31d5add84518f68e30163f53aa73a62ddc0b94fe0113d711b73a5f86099d56727452707e524c3f71f2c1934ae24431e89f13679985384be002c10c27203c713e67f761e07d80cf77f16f102d5016863c420425f2ad3140863e27f11626f8f916ae4b34af7cd79936d74dc585d5326e4d41c215c26fce5ce713d91cdc87b0923747ee0ac709fc052298000cfdcf006398200a03e61c937617f6222a8ac94cb621d1316221ae730d9e3e42b4c10f0bf7ed6c468ce27df0047d365462f10505c4a2c82a9c64508165c7ce09a4185f92bfd8eef4e73b623fd6b0fe99c19472a39b73ed70889525d6303e81c760930b438395ebdcc2309f903e04dbc2b827ece3d7423353c2e2248e6ee3905bc8be7dbd03c9065221c59a09ab384b3c0bd428a14ab4e807154afcb8844ee3a30a95f9b4b6df56abae596668d76cc137cb16f7ba0b70085becfaeceddb7d98f5254cbd24d81642d46e7e6aea54c6b4d74aedfb9a2b3301adb9320332c5ec2ede065e07626c8e02afe2931eadf2bb2d56ed4e844197dd48f545ed48eacfbd5c494e94b11fc9677f7322e25ef4efd6d8e0e5b990cee3bab79d694f6bc10e6dec4c8d546e2f3242dcb0dac45a1031b6afc2fa0dcd8b4efe665de25e1fe34d803a4c6ca8637aa753cf281a148f1565a8388c111a1f2c4a9ce4736780f3d7c00495c8f43873c9d03aa0b19d0e9dc6240127ee9b1a317e820a3b2b07516161e127ca35afc55db4bb805b08c2831cb2ef221084e3a7079f1f621c7f49da76263f0e901a107124f1de7721bedfa7c760ef6d2cd9f831f640477cb423813b46fba62e47198ea87c9289cbb948d3d80b727c84e3a33ab49ac4bd43bed395b315fce4433ec256e271822df13104091279baa22fb941973084183a175e715bba04a9077c99bb8410df18a58a57354aecd6b389d00fd23ec4a9c1840621205c1261113719202c7bc00079c116841d3c11d619fa4e4a6db583f8e5126ea9e520dc41bbc9d6fce59f8c42b59320882e0ecda79c76b52b5d234ad249571d32d5922dcc4b543451ae1a78636b8a8a9811b44445b3c9b6ec9ae870da51c7acadf25404b03d354471d7d2ede086d41289a4b1f444a2fc65684a994ab40a55cc2bda754496a61a9c4241ca3bb45678b604f291422b5aca4ca5a73928439126cc2a8ae902bdbb4d28281bcb2a140cd87deb116c42bc76eb0c9ef4b661eeedf46087b69122aea58fb08445302989ce13dffc65f6092b526722c3327b4ecb205e449f025d412cdef0d451b4102448f4370170372e12889aab1d40d81d8904a16460c7f2f212545a5db80183c5a5d6eb6054662f80e2042733a4be1be3a6e95e8c44f33148462a3a1a32900cd12087e02055388bb9499354033634af076516e803e3eed09843b8147d0bd9ca60787f2473b2d010ad89f74c223e7bc2e518cf446f76259ed9b18672959fee8c60ec6916fc310780e1370cb50dd7a021591bd198c534f6c4aba218014aeded7af3417af96af1759dd2d79dd3bb7bba013581dd3355e771f56d3e2d2af6ca81f95453da37bf06db6d80365ca95f39e2ac8a3abfe58f2bfb6ab8a880316db2568c1eaa9d709c800d14bed25a2f1fbe0f92145f020cee01bd0b5cfa9134ad117d68ce55b65533c090a5c50e5a369ffeccc5382c009143d072c17b42484443d8fca54df2e8f24287d6568210248a7bfa651c6611d207b4fad555951c0fa21ab480c3d7bc3560f11fcce1f1575f3cb8b62b313d3496fcf290740931b5014146527a20895fba0b6d6a9391aeb59dac5dda56c5aaf6faa65fba1f8d2babb77800e590398c461d050fa9f1e195e987f604ea528e220db1d70ccdbafda845f154c8af2f46f6af140712671111d94b334f0bed85a95eb61f5972d51f3c106ef8a8a4504594bb094313201bca44bb7a3fa2a95fe57918f5a839a4f2999d07530e99c3a8dfcc7930f5a839a4f251bcc19862c81c067bf2e681b0311be5670fd84ddd67a3e3ab7e332b90f5bf4a7f54da5d7d5429aff284a0a98ce27c15f222990b46daae7fc6e076b543487b596c84500ec40e23314fb3169a94b7eac241adf0d80453016a92dbf1786684553f49b6bfb6ef2e4c29f716a754f65fe5e042ae3d2ff3deee5e3b29112ea6b80ec1fd31f06912bc7a4e318c2674c264f57bb80c833cd56313ae010ad6841145c1997b363b3d137af78ea78f6e9aa67ea8b837909be99a221ba1662ea03ced2ca2b56d2c6bb6b4a14790780f20f96b049ebe1fa24bcd273fe361bad472faa5f79d2b72983e2ddc3ff365e7ced5bfbed62b4f9c9b84e8dfb93373feddb3bdcd74f36295c5c6f6dd5eaf43f11a1d574cede4a2cafedd104ad8453f440ddcbe7feaf00aa138685fac36f055bd4a81bd9d1d5017ecda3c5e8744a4768b1d6d74a70af8c3b0d0c09a7b9e296201fb50aadea84f1fcc998ae5e7dae0c0bac4bc17a4661979bf88452c0db740cc4ab177f6d5dae4549eaa4981bacd60a4834d1103308ced762e97ed33c8da595efa3cd4a616bb5b66a3b1eee091f91efb190ed9c0c0aac9c6ee5718a37852fb50fd92fb117ad5d38e2d6dcd8bce21257df86a71b3be825e72b6f22c2f4cd6561ee4f0d26eb956b697f85e8e803a3014113029933e7675d13ee61cb7bea82efc3999c9f252caea55698cb91447559996279fb174c6b43da7772f4e55556bd67f339b4c066cc1194bf4faaa87e3edb1e93a5f8e21d56826b52a44f6e93dc64c3c5adfa08fd08998bc361fcde17358351aed10b2d7a1917358751b9c5caf2eca51d5e7d6d2e6563cf32f5cff9e5edb16d733bad6205d0f5c6b0b9c6a037d038eb2434edf20a702ae697150f5bf697be75470958d286d6d755d5d75ba4db47d2aaabd740ce2be699964469160f65a5177eda617fa98fd84cab69ad6ae9aa609f19d1647d52d28b53b1d96a8e1fa01ed08b39273bf7ebfaa51abb5ed4f53f1d68b48c9f085baca61c9646d64ed64aa4be8e4b0466a1338024287ed68b497a570604825de83372cb6a8ae59c1e6209e5579b228aa4c87275fafd26695a13dd57a78c22dda34e5925012a572ff49030995d3605383a0ff6503825e233eade65ca175cc4265012336457857bc8618f82478bd4870b0061e269f3d98a6f93fd5f62b083348eb1ceea17f856e32bccd30211946f761e31f0ea5e176dbfe792f6a13e7f94d5e22940e41024133a0158f37e81f5910fa15deef15cf9a1a10348e2fcb78a82c312de7d93c57903ec5c81050c9be2afdb883d13624c0d21bb4028f7017dc88e7f90837c07b6695bd7a20dd8268b27d7e19804d02a2b48451af27bf121df6a3a79ffe0fc99b0a11ab640000	6.2.0-61023	T
201801030700092_RenameUserToAppUser	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed1ddb6edcb8f5bdc0fe83a0a7b6f0ce8c1d18d81a33bbf08e93c2d8384e3dcea26f012dd163a1ba55e2786d2cf6cbfad04fea2f94944489578994349a491004086c913c3c779e439e93fcef3fff5dfef41285ce33ccf2208957eee96ce13a30f6123f88b72b77871ebfffc1fde9c7effeb47ceb472fceaf74de1b320faf8cf395fb84507a319fe7de138c403e8b022f4bf2e411cdbc249a033f999f2d167f9b9f9ece2106e162588eb3bcdbc5288860f10bfe759dc41e4cd10e8437890fc3bcfa8e47360554e70388609e020faedce09738f9cd752ec300e0cd37307c741d10c7090208a376f129871b9425f17693e20f20bc7f4d219ef708c21c56285f34d34db15f9c11ece7cd420acadbe528892c019ebea9d8311797f762aa5bb30b33ec2d662c7a2554174c5bb99771fe1bcc5c47dcea621d66645ac5d159c9f95939fdc4293e9ed412c78a41fe9c38eb5d8876195cc5708732109e381f770f61e0fd025fef937fc17815efc2904508a384c7b80ff8d3c72c4961865eefe06385e6b5ef3a737edd5c5c582f63d694245cc7e8cd99eb7cc09b838710d6f266c8dda024837f8731cc0082fe478010cc620203161c937617f6c22a8af04cba21d6316c21ae73035edec3788b9e56eef962818de25df0027dfaa9c2e2531c608bc2ab50b6830a2c3b76ce20c1f80aff4577273fdf63fbb186f58f1dcc0937ba39d70e075b59660de303780eb68530046897694a00bace1d0c8bf1fc29484bdb9e55639f4bbdcc3183b324ba4bc266191dfa7c0fb22d24124ad4e39b6497791668516629f1a2832ac4c431093369820ab5e5bcb1e5760ba7ec3335f172fe371b17f77a9f6c8398fcd862e567e7e7a318b9de18a83e0d3306aa513a63a02a696b0ced8831b364d4ea412d72cd0c5bf4ee9334f0da71a35364c4ca112d56d5b02d4a642dfef41884b0152f6e9e8c1c33acc5909da342d3d895342ecfd097d005df9c89b8d77d80c2291c89b0ed15ccbd2c484b216a373f358d55260d035a3c9fd5712b9a89f63c1e334031747dba2045768e23b8be9a70d9f709437a9eb5783f63b752c030f629c5ec6f0e45dcab233039df4bf2b15f77d233dab0506b6de42da8bdcda13e30ccd07980168c8c0d8d8b250ccd8d59f3b5185dbb4edf41101ec498de462008a70f093e3e2528f994b5ed8c7f1c211cc0e2c892bdef823da84fcee2c1db58b2f17de2810e3fd893c041718745faa0f33caa14c3c4fd5ce679e20505623cc27594c6d3f936f69d8e6cb5e42c431fe62f763d418a9d0dc6001f3512f7f4506b7a19a8d56d280ff4af1250eca16046ce6510ae31dbb1cf0b6224bbb320f6821484ed5409cb0cfd20617bbd81387205536205316aa7dc64671ac3cbbbd79b08feb98b37cb39a318edfa2285f53ad1ea63fc46b64df22aaa8c48da6d7c054388a073e99597f66b907bc0972d132bbe6f8cd2d4eaa6e3c904faa6a3dd646bf60efab04a57856a9d021653a46e959325ac07ab509b2a5f6a856941b09ca2767950c555dd289e599107f761e620ef2cd136a17f96e83f7a0f2da4355d02d69b8a466bda2c459738eddd50d898aa0b39e5fdec28c6a20cdd1ac85ce0d7057d90c528489cd066146ce8bffbe8165346c6e47518af8059b5799146174fc62f4891886392aa5c3caf5202511708d00d44e2cd6813870b0186a44c3c80eada425e4eb5b3633d731f23c168dc7607107a332241a8acd880864a0f54403873104031b263982a3ea631b334ef6da23a7527343511b50c249decce5f182035b262a0ca936840be7ca32ed3df1e9e9b05e80cf28c16b570411b57ef970d543b5bb8a03a038d22c6fe3c108e3e0610c5773007140f187a4bd084908641642f6b90634613760e60845e13da82219370a817fd13aa00e743f5e46bc323e300a9172354f19000a8391f8c79422fb7ea23bc1e5bcecbeac3eac372ae29535cde80340de22d53b6587d713665cde2fafb8d7d655f54c298f38a26061cf54e28c9c0160aa3a46ecd87ef822c4757008107402e23d77e244de30216cd394cb7e26312596ef460a6f3c9cf4c584463163988ab16bcc38444240e2c1ecc24af2f2f74489d280841a678285827e12e8af521a17e755df1c782a83f5ac061ebf73858ec80393cf6fe8405d776afa28746934b16922ee1243620c8480ab025f14b176abc3619e95ae9237a685a15def65035ddcafde81a5379c602613e1f8d2cda22802e79d4a982bd40f44bf72391aa7c8705507d3287c13d9eb390b881afcc56b5b15197729459a0bd6668d6ed472d641bd599e7d84a7140d75b4755f6426d09150d44dbba7a3f026e8a035818cd577348d56b3f0ba6fa640ea379ba67c1345fcd21556ff31c63ca4fe630e8cb3b0b847eb339ebe83b3a7fd4d1afd3ab3e9f1ba88fbb3a2bd49d69f504ddc9a5f45324cdd1c47742f22873c548fb0b20eabb50666b7bacb437b14658b5bf01ea1013b3386b618a19be265ed48ab21a3795a4fa16a027cbf4af40567254df2a4ca75cdd32941272714aed0eeac45c48c0975532dcdd4c2865c7e514d7c1b83f073ec98c37af3982d18c4c986dfe1daec3a0c8ffe8841b10078f5899cb3238f76c717a2634271e4fa3e03ccffd50719920770bf2229ba0922f203ced2c90b5ed9ce37bf6e26790794f20fb73045efe32461b9e8f7f46e3b4e115f44bef44d7f86c7d59b9bf17cb2e9ceb7f7e6e569e38b719d6bf0b67e1fc31b07fcf74f37295c5c6d62d6c5f87de497d6454f3e46acf71d8a74e8ebf58fe71ad333d7927435594b22b6197c5ec0d70fb4698c3db932242f962b5a1d390ce1707d4855ea5f987118c81460e3c56c4cafab1c4c515ce8fe610c4ba786d7c605dfb3e08125fdf3e2c68116bd62d10b33e7c7afb1c7d1661533cda6d0ef53e7636512d1b641813077cb671576fd16993526329778b6da2b3f580fab2b7fe8ac195885575d9c40d14935417ea1ea18facfed6ba43e2902d1153eb4bcb7be22115e6d83a1c94378f47a63a7c14a142c4a4d6fb4b50a4d6f700cb00e910caa4ba326604290b5029b8af528dcc453ab90e1ddc275937210d0d5eacbcda9717c058b9ac236a21ea3a8caca4bf9f28864d8a1468ecd3834c1901b73c661ebd121df5217448059aec08b2569fc346313d5afb861e405c35bd59e3de62361bb7776f0a4d68aff13ab2ce3cb91e5e149faaf5aea5f3aeac1858b9fe03b9fe2def7874ed491270aa54fac63c25f872b01b7e13edb474eda976d0b7d5289bfaf43d7d2ae09ab695967ebfae763fd52eaded203a46e9a811c6b55499f38d3f0a74f2efda4fad0693b53bf23d79ea5e52e50591dcd471b40d8dca76b3c31329749675762d6aaacf462158693d8adac7f1c9a76d60dde4ab0bdde490512a2c3f0ec2f7d5a4696dc1565a32b809b345bf8d8a2bc770590a472d17838e4eb85eb38d2a38072af6d444efa3f1d45ad28a8043ea42d84b5fa95cae8ac35ee67fc8c011771e6c1b10e4ffcb88a1c705bcf59cebf831a131b780119d22bc73de40047c1c0d5f662878041ec2c31eccf3e21fb7fb15843b480a301ea07f1ddfee50ba439864183d84dc3f584ae2f7b6fd8be6591ee7e56d517f938f4102463320d598b7f1cfbb20f46bbcdf291e5b3520486250552d11592252bdb47dad217d4862434015fbea7ce61e46698881e5b7f1063cc33eb8610d7c0fb7c07ba555c77a20dd82e0d9bebc0ac03603515ec168d6e35fb10efbd1cb8fff07dc6b620e28660000	6.2.0-61023	T
201801030735349_AddUserInheritanceToIdentityUser	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed1dd96ee4b8f13d40fe41e8a724f0ba7dc48389d1de85d74762ecd89e4c7b16791bd012dd164647afa49eb111ec97edc37e527e21944449bc4a22d5ba7a122cb0688b64b158ac2a56155935fff9edf7c50f2fbe677dc151ec86c1d9ec70ff6066e1c00e1d37589dcd36c9d3776f673f7cffc73f2cae1cffc5fab9e8779cf6232383f86cf69c24ebd3f93cb69fb18fe27ddfb5a3300e9f927d3bf4e7c809e74707077f9b1f1ece31013123b02c6bf1611324ae8fb33fc89f176160e375b241de6de8602fa6df49cb32836add211fc76b64e3b399fb53107e9d59e79e8bc8e44bec3dcd2c140461821282dae9c7182f93280c56cb35f980bc87d73526fd9e9017638af269d55d17fb83a314fb7935b000656fe224f40d011e1e5372ccc5e1ad883a2bc945087645089bbca6abce8876363b0fe2af389a59e254a7175e9476a314ddcf29bf9f77dfb3b28f7be58e13c648ffdbb32e365eb289f05980374984bc3debfde6d173ed9ff0eb43f8190767c1c6f35884084aa48dfb403ebd8fc2358e92d70ff889a279e3ccac393f6e2e0e2c873163f225dc04c9f1d1ccba2393a3470f97fbcd2c77998411fe3b0e708412ecbc474982a3208581338a49b30b7311164d48cf6242c263444266d62d7a79878355f27c363b3938204271edbe60a7f844b1f818b844a2c8a824da6005960d334738c5f892fcaf983dfdfd40e4c718d63f37384ea9d14cb97a38e7eb3511b4a802a3a0c7e1d1db4ec87187beb8ab6c0fd548ccac0fd8cbdae367779dab847ddaf62967e798201285fe87d0ab86154d9f1e50b4c2e9c686eaf665b8896c03b40a1a2bf12a1a5588896d12665207156a8b79a502ea1543413e5dcd90f7df35d5d023670a735ef9c8f56aa63d3a79a335adc62c441b3db9918fcb55fe18120e418131ceef511c7f0d23e71f287eae419dfcec00f525b6371161c26582fc75efb3bd7f0e037cb7f11f531e1f6eaeceb6e6e16b788d6c726a5d05e9a8ade1bd0bedcfe126b90ab2a3e46362cba78926804ed039b76d1cc7d78499b173116eaaa3b5dd8994aaa6f45707f2d7fe402a74fa760752a1d5a103a9381674d1baf090ebd76355749191ca5b409c68b3294aefc2951bd4a354749151ca5b409468b3294ac5e15a8f15d34b46ac6c0471ab7a98a297c2a9478df690d1ca1a4094f25653741ec2b56bd7e353749111ca5b408c68b30a256de3a630eb5380198fd69939b7a577751eafef70b25f8cdecfe15e47042639253fef4b60f72cedc195c574a46b311d1f3e3e1dbf3d79839ce3377fc5c727dfa26335981fa1d28ae9a4bd1b07d94c3f236fd3f554ada421538fdd4b430676fad290a1493e7f719dd42c9c378f283a13f05afd0b7e36953901b3a1c5815be6d0930fa303b4c5a5f2e0355de362c0aef9c6fd6bf70737f1ea6df2933e18ea12c776e4aef34d84b94a3762b70bc1b01adfc328e8245a656054aacb309da6590d85ea64c3bb033bb65cb86cc80a4d30cdba30653318daaa28ebfd7f3d24ced5101a38e92572dfaf166a12b82dbd3348d60496efc6bd36903530285e83582b433905dfbd9d9c429dbe99ac3662955dd305b591feb1fcbe02df89d89ac5c677cf6dbbc169bb72b933766c19d6e8ec7e7fa2dd2af529b74ad68aa28b89c1721ec7a1ed6658f10750696ef24bbc0a1cab21f09d539939ae08ad092fba6bc27d0403b2c912e160a8e581c140a56f2278a07f91801236c5514a1c94deb8c54408dc209179da0d6c778dbcfa5509c3348521257b3981d87289d7384837af7ee53a33333e8c8c40398f20a74de459cc19ded063197a11d1b4b7e2ad8406c388abba0f2eb187136c9ddbf9c39d0b14dbc8914592b0bda3898f82d51481e85eb98ea7cc804cc7934067e249301cbd666ada60f1ce696c86132eb90086a3b1de5e198ea7cc800cc79360f20c270561a02d862332d5265711ca41b80e7e9934d0990ad164007683d6ae3335fbdc6e5ca6a37e75e3068b01ad669693771806ab601b1addaa85d942ad57e186264daab8b4efc4fc54442ddb10732bdd2cad6d40f52cad7f97ccd0fc2d45d3160b0f2bc6b609f8971c804990c7197ae53a8e2c03721cb7fec9db034220b6697361c50c305b9d5e8642bdbda965458c02c2ad2e6021b3b48a9d7b12b39a30c9f0b206536900818329a133b93a60dc8bd4e501ab3475838cc0114520bb1bcbf2395e1245c8952c88465d631ab513392205bac48978f35a85c704935892431e008dd9c9c30bc16e182f851854c014710803b0c563cc5ab0d4db6c00cb5c0449d02ab3a80148714d2441a07acb6069f4d964edca72416e00da04480984615c86a3c4b7c24c2fe039b1284bcd41d612fd920125816c8ea932404a644595ca2fd160f9052fc3ab57450b75e285add62ec4f818182a01ec8c0a85e8c1545085b0748258ada820049e002a14486f4d05f9e98a4c87fac88a5e6c855907a3a06ae80186447a1109e97ebc860a2a8352cbd96f4f03c18e640015f876260e0c66b04400debfa6ffdf4a2e64775f879c5b10821e373011143eac8617db6af1bce709e8048a7067048045a1ceb5d271ae5ad160001950ddf4caab6ff2b574bd2d6605caadd3758e3a6287e2fab9b4e6cbb6c53caf12403f2ce6403981c52d5aafdd60c59417a05fac655e5be0e2bba57906be9fc398f39c26fa1ee54c4918a115165ad3fc72075fbb519c5ca2043da2f469c085e34bdd38df05b0478ba978f744debbc24e2dfaa7bf190fa9705f649f8e0eb8260bf153af307b9b279d7bf2402bade7803c1429de735c84dec60f60f7141e5d66e6b320ca8f0670d83c7b0e16dba00f8f0dfeb3e0ea2e0560684c6494055613304d2541d829c9e39698400a87f03ca5c57190ce69e437eaefb6603868643f1c4713bd5900f493210c26575802c6b4e943e5d3b959987c8b3e4421679b0529341960c966667348b20dade0011455f7d09f41cec566a1cbadfa901559d92c6845730bd80a9cc536034d24276e731a496ed6879d4a71fed28e05597d9d8c8e53b8fce60a4f8ec999ab3e0d18fd2841d569041f45e0a15b656172476ef5d91016cdb39480d1ef936420308063c24079f4753b060260c0fa854b59e4d54b6d9e250c93cb43e454785d1e260ccf8c4d476286ba7845130f943173f3ad8787f6a3326832207774e69ff4617039352c24aee19bb4abc17046138be49722e6fc018ceb8739e4631f3af2bb668d099c0050accee400c8ee97b6d3ff6a10fd5a00c575300b05ba221e6dafb6dd9f2df7c66c5f8692c3bef7820f05aa4fcc320a0c1d8b6507e8f0532ab934b20984738460b14c15ad3dc980a85f4130539b6305bec3d0c2aafebd2a849818b435de4c31a20f0486c0ada4edba3ba98efab72419fc86cc681fd5b708c33157f31e4af177b14ba90eca38bc106f5fd0d877738d5f29189e779959851370365bbec609f6f7d30efbcb5fbc0bcfcdc2bd45875b14b84f8499f344c5d9d1c1e1915033783af57be771ec788abb03b9882fbf6503e457ba294d1b53ef4d2b2ff1a574832f28b29f51f4271fbdfcb98beab80ef99d74531d375bbff442ec2670f0cbd9ecdfd9b053ebe65f9faa917bd67d44f8efd43ab07eddbeac6e411b2d24cad13c0e626e6c3779cbca90fb102c5991c468613248ae5cab126a96d45b416d5f9df5d14d3aa9cc0a8a4aebeaab5b41545458ed0a5e2724842aa8b68105564f55691b9dc5aaaba9b6410daca49a69af2deba8ea6ba06264f679cfba893f06ee2f1bd2f040a821282241b2ba28a00004c077f694dce220d8fa14501cb362b5c4ad045dae886800aed3aa8725f17efd1fa826d8d9d1a92816d819ec31f9debc42e0b7a16b1ed82a7d802974d245f92b35976405b0cc34c80e59ca8ab0c4ce328ada32100aa98dc6265d96f9daee6000a3427dd5cf1adb3ee0ab6a1961930f1d4212e170fa4e39aeedccf374d12398e73561763da2c3c14c937a0bcd1b55ce6316f9a1c3b60afb0c1c77328df1b4de3a032d08ecf2aea9c1c198a8b7626c5b97bfa039af03575b1ba4ce00f41e7eba752cf4caa98d58bb4291963c5ef1b42173e7ebde7c4eb452855ea9b48930130df88c57186d686682de7f4e8c998ccba08d59f76ce8c3ace62de798a7d9d4ca98299f6c4c8c7578bf4785884e899d5d60a4da8754862edd18cca47a6bc36ca4bc81ca8dfb26d9487f4b07e7a1d1755243ad81ee3d2b23adb67bb68c91ca9a8a7fa55527702216711ee81ead2ce0d0f630f0e47a62e6b0c65bd4493114171254a0d1e7693464a8a7e645f1e49968d206cd980c34983963cc3ee35ac42dab734ea520e798875b4382cf344eb8d12b6ecac56dc43da5b91cfc7377b8a2669e0f7036731e43b2ebf92d09546d4d025e1c9832f4a245093efbe79394f5a09a2b72ea15e4ac9f16a82c5837370d73d6ce4dfbd4cf0dd4f313e7aebc1169caaa4935135c204d9c829e0f127cfa5d051ca8bf5547b85c63d4d22def524f36758d2b68e286499b26d49b4c081b80fbd4444ffd1de30f7648e89ae6530bde60e553f9ba8eeac2bccad0313b4c5d0e780a05528d97076a38a02849778beeae1eea568be6542b504863fb45f754fe54599c72347686ea9136d6380552373b59b0524f2a1287bb5f7e5137b179f9ea2c51d9d5639057223dd2c2e5086a37255d8de5da884b2650b2752bc5c55a19ea0210dd2db851a0b552b1bb388d1536889c3adef9c26151d6caf7de5292875a74cf656995ccaa62d44158dea00cad9cee4e1ceb4d90260be67f5de2d85d5520160466806dcea52efbdc044f61e1d90b18155d84b788b738410ef1b7cfa3c47d4276429ad33cc1ec5faecd72afd26cd547ecdc04f79b64bd49c892b1ffe87179496984a06efeacd62e8ff3e23e7bca1f77b10482a69be657de073f6e5ccf29f1be563c880440a4a1079a0091ee65922642ac5e4b487761a0098892af8c983c607fed1160f17db0445f701bdc08fbbdc32b64bf56895a1090e68de0c9beb874d12a427e4c6154e3c99f84871dffe5fbff02330cc641ff990000	6.2.0-61023	T
201801042244105_AddFirstNameAndLastNameToAppUser	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5ddb6ee4b8117d0f907f10fa2909bc6e5fe2c1c468efc26b8f1363c7f664dab3c8db8096685b585d7a25f58c8d60bf2c0ff9a4fc42288992782b89d4bd27c1028bb6481e168b55c56291acf9cfbffebdfae1c5f7ac2f388add30385b1cee1f2c2c1cd8a1e3064f678b6df2f8dddbc50fdffffe77ab778eff62fd5cd43b4eeb9196417cb6784e92cde97219dbcfd847f1beefda5118878fc9be1dfa4be484cba38383bf2c0f0f9798402c089665ad3e6e83c4f571f607f9f3220c6cbc49b6c8bb091decc5f43b295967a8d62df271bc41363e5bb83f05e1d78575eeb98874bec6dee3c2424110262821a49d7e8af13a89c2e069bd211f9077ffbac1a4de23f2624c493eadaaeb527f709452bfac1a1650f6364e42df10f0f098b26329366fc5d445c92ec2b07784b1c96b3aea8c69678bf320fe8aa3852576757ae1456935cad1fd9cf3fb79f53d2bfbb857ce38118cf4bf3deb62eb25db089f05789b44c8dbb33e6c1f3cd7fe09bfde87bfe0e02cd87a1e4b10218994711fc8a70f51b8c151f2fa113f5232af9d85b5e4db2dc5866533a64d3e84eb20393e5a58b7a473f4e0e172be99e1ae9330c27fc5018e50829d0f28497014a41838e398d4bbd01711d184d42c3a243246346461dda097f738784a9ecf162707074429aedc17ec149f28159f029768146995445baca0b2a1e708a7145f92ff15bda7bfef89fe1863fd7d8be3941bcd9cabc739df6c88a245158c821f87476f7b61c72dfae23e6573a82662617dc45e561e3fbb9bdc24ecd3b2cfb938c7849028f43f865ed5ac28fa7c8fa2279c4e6ca82e5f87dbc83620abe0b192aea250459858265126555091b65a5626a0de3014ecd3b50c79fd5d330d034aa6d0e7951bc549fab3d6480cd1f37b3451c7ef7ce47a35bd1e9dbcd1ea56a31762801fddc8c7e5c4fe1812a5408131cd1f501c7f0d23e76f287eae219dfcec81f435b6b711d1bb7582fccde0bd7d780e037cbbf51f52b51eafafdea6e6fe6b78856cb250bf0bd2569df1de87f62fe136791764abe7a7c49617504d805ec839b76d1cc7574498b173116e2b6fa2dd229c5ae306add7d5bff66b70b18c755b838b850c5a838b955097ac0b0fb97e3d55451599a8bc04a489169b92f43e7c72837a928a2a3249790948122d3625a9f027eaa9626ac9849585206d550d53f2529c7ad2680d99acac0024292f3525e73edcb8763d3d451599a0bc04a48816ab48d2f6e78a9d4c0a98c9689d6777536e28cfe3cd2d4ef68bd6fb39ee554430c92af9cbbe04bb676937ae9cc4235d27f1f8f0e1f1f8edc91be41cbff9333e3ef916f792a36d9d545631ed7470e720ebe967e46dfbeeaa953664e6b17f6dc860e7af0d1999e4f317d749ddc265738ba23281d7aa5fc8b3a9ce09948dad0edc30c7ee7c1c1ba0ad2e55d042331a5034d8b570c0f0d6fdde4dbc7a9ffc640881bac4b11db99b7c1261a9d20d52ee42fcaf66ef61146713bd323010d7676452d3ad86a293b2e3dd831f5b0e5c7664852298677db8b21986b629ca6affdf0e897d4d12101cd60a35295cc7dd19a46b82c8f7b3bd36d035f01ca086b0568e720adfbf9f9ca2cedf4d563bb1caaae980da68ff54fbbe82de99f89ac5c4f72f6dbb2169bb729e35756c19b6e8ec7c7fa6d52af329974ade8aa28a89c3721ec7a1ed6654f10b50e96ef2437c17385643e03be732b35c115e1359743744fa0805649225c6c1a8e582c1a0d26b203ce89f245022a6384a9983d213b79828811b24b24cbb81ed6e90573f2aa199a632a46c2f3b104b2ef10607e9e4d58f5ca767660f231350f623e869137b564b4636f444861e4434cdad782aa12130e2a8ee824bece1045be7767e57e902c5367264952462ef68d2a3103545207a50a9e33933a2d0f12cd0e9781602478f999a26583c739a5ae084432e40e068ac775081e33933a2c0f12c98bdc0494118688ae1884c35c955847214a9832f638db4a6423c1941dca0b1eb74cdde309c56e8e8beba7182c58056b3c8c9330cc32ac48646b76a315b98f52adcd064491587f6bdb89f8aa8651b6676b2cdd2d84634cfd2f877c90dcdef52344db170b1626a9f80bfc901b804799c6150a9e3d832a2c471e39fbd3f2004629b261736cc80b0d5d96528d43b985956c42820daea0216b248abc4792035ab09938caf6b309746503898133a9dab03c683685d1eb04a5fab901638a204646763d91396974411722503a251d79846ed44894841d738114f5eabf098e0124b7ac803d0989ddcbc50ec86f652884105a6884318c01697316b61e96eb30196390892d02ab7a801a438269210a8dd32181abd36593bb25c911b409b8094208ce0321225de15666a01d789455d6a0eb296e49702282964734c95012989154d2a3f4483e117b20c8f5e152dd48917b61abb10e36330540ad81b170ad583b9a00a61e904b15a7141083c015c2888eecc05f9ea8acc87fac88a5e6c85190763a06af8018644065109e97cbc860b2a87526bb3df9e07821fc90015f4f6a60e0c65b04600bb7fcdfd7f2bbd90b7fb3aececc008badcc04c50ec613576b1ad06cfef3c019b4009ee8d01b02ad46dad743657ad7830820ea84e7ae5d137edb574775bcc089453a7bb39ea491c8ae3e7d29b2fcb56cb3c3102fdb05a021914563768b371832726a302fd62adf3740a17dfadcd930ef839c692973471ef51f69484117ac24269faa4dec1d99bd94b94a007945e0db8707ca91ab77701fcd1a22b7e7b22cf5de1a716f5d3dfcc0ea9d8bec87b3adae08a0cc44f7785d9dd3c69dd931b5a690a0be4a148719fe322f4b67e006f4fe1d665320216a2fc6880c3a616e0b0d8027d3c36f8cfc2d51d0ac0684c649405ab0998a69a20cc94b4e39684400a87f032a5257190cd699437badf6d217050cb61248e79dbce82309ff5b1aad7ea2c54f5551f893e3f6761e827430ce605b304c694e9a3f28fcc594cbe441f517849ce420a450654b2efc53922d98256780047d535f47b905f88b3e872a98154ca6fc539f1948b5b602b6816cb0ceca3fc9c9cb39372b13e766a5b641dadbecec6f22a0211e666588e149a1b640d8c614cb36a8d841748d015a8de86728e40f5d9108bbefe94c0e8f7590a1018563211a03c26dc4d80000cd8be700f2979f352fbfa13c6e45e477226bcee75288c6726a61309435d14a54906ca48bef9d4c34d873119f48922b774e69ff431b8973e2c1257f04d7afb6090a54944f2a31a73f900da0d231cf2b26fea96b7158d19ac005004d16401c84ebdbad97f35c4b01e407148cda24007d793cd55d7f9e9383766f332961e0e3d177c8052bd6296b16968592c2b408b9fd2c8a5f15620c82484b065ae68cd4906a2be9bc1746d4e15783b448baafa5bb410616228d97832c57306205c054e252dd79d49f559444b96c137db8ce6517db6319e7035cfa1742a205629cd41793a209c02ac6844be39d9b214a2cfab2cac621370b658bfc609f6f7d30afbeb5fbd0bcfcd82d045851b14b88f4498f3e7938ba383c3232179f37c12292fe3d8f114271a7236657eca4678f5e9a63c6d4c08609a0f8acf691c7c4191fd8ca23ff8e8e58f7da42976c8efa49f34c5d9f8a57b6bd781835fce16ffcc9a9d5ad7fff85cb5dcb3ee22227fa7d681f55bf7fcc6056fb488285bf334882f76fb794dad3c081843242b96180d4c8694f2e62a91d37c151dd3e2f685cb65bd5582666fa32bd4f6496e1fdca49704b7a06eb74e62db095191a8b62fbc5e580825a26d830526a15599479dc1aa93d2b6210d4c489b99db8ee968f54d66d132fbbc675dc79f02f7d72d29b827dc102ca7a0597de4a10022f63bbbac7758b93a2f5b0abf404c3ad949d1e5c4920670bd268f2c99f7dbff4052c6ded67a45cec5deb0a7947bf3448bdf86adb967931d02aed0491f59c4d45292e51133b3203be4da2be2283b2b2843f8e1fd89499fd9d2ba2d0c60186ba8346453fb077c7232236af2a66368221cffdfa99d763bf73c1df404ee79cdb9801ed3e1e8ab49da8ae6892afb310b55d1669de2542307ca4c8352ada7cec00a02b3bc6b667034211a2ca75de72c22f4e9f0c849eb4649d7003d2b986f3a10bdac7413a60051bcee9e2e07dd982908ea2ea9ce34e1875ec6b99908130df84c975f6e6c61822eacce4c988cb3c94d993e6eecc5ace6f2e994abd9dcb2c129ef98cc4c74f87d8f8a109d4c45bb2048b537bf0cb774530893ea72103391f2042a27ee9b1423fd291d5d8626b7490d291bfadf591959b5ddf3658c4cd65cf6575ae91667e211e781eec9b22b8eed0f0377c467e60e6b5c9e9d9540712141051943ae4663867a6aae40cf5e8866edd04c2940a3b933c6e233ad47dc32c9e95cf29a4eb9b835bc489ac70a3779e25239479038a7f4f1097f3f1f4e4c9a3f60385b380f2199f5fc94044a5a2781170ba68c5e9428e1b37f854a9956ab39b1a95e5ed3fa6e81048d757dd330676ddfb44e7ddf405a44b1ef6a3722755915a97a82f3cc895dd0f541c2a7df55e0401ab33ac6e516a3966f79957ab6a95385411d3774dad4a15e6742d8009ca7267eeacf18bfb0434ad7d49f5af146cb42cba7c754e73756868ed966eaacca73c8336b3c3cd0c2015954fa1b747f69653b0d9a33ad40e68fee831e288bac32c7e764e20ca5756d4c150bbc35ed65c04a3ba978e9dcfff08bf493cdc3573f6b95b77a0cf14aa2271ab81c41ed2733aeb15e1b49c90c32df76325cac97a1ce58d1df801b155aebed781fabb1c20791dfbaf73e705895b51ea877d4e4b1063d70765fa5b0aa0475149137c8e62bbfcf271beb6d903e16ccffbac4b1fb5441ac0866806d6e4b5dd6b90e1ec362672f50545411ee22dee0043964bf7d1e25ee23b213529cbe13ccfe01e0eced55fa5af5013bd7c1dd36d96c133264ec3f78dcbba4344250d77f96b298a77975975de58ffb180221d34ddf57de053f6e5dcf29e9be525c880420d2d0037d0091ce65923e84787a2d916ec3401388b2af8c98dc637fe311b0f82e58a32fb80d6d44fcdee32764bf560fb52090e689e0d9beba74d15384fc986254edc99f44861dffe5fbff0230b3c9ff399c0000	6.2.0-61023	T
201801050313086_SeedAdmin	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5ddb6ee4b8117d0f907f10fa2909bc6e5fe2c1c468efc26b8f1363c7f664dab3c8db8096685b585d7a25f58c8d60bf2c0ff9a4fc42288992782b89d4bd27c1028bb6481e168b55c56291acf9cfbffebdfae1c5f7ac2f388add30385b1cee1f2c2c1cd8a1e3064f678b6df2f8dddbc50fdffffe77ab778eff62fd5cd43b4eeb9196417cb6784e92cde97219dbcfd847f1beefda5118878fc9be1dfa4be484cba38383bf2c0f0f9798402c089665ad3e6e83c4f571f607f9f3220c6cbc49b6c8bb091decc5f43b295967a8d62df271bc41363e5bb83f05e1d78575eeb98874bec6dee3c2424110262821a49d7e8af13a89c2e069bd211f9077ffbac1a4de23f2624c493eadaaeb527f709452bfac1a1650f6364e42df10f0f098b26329366fc5d445c92ec2b07784b1c96b3aea8c69678bf320fe8aa3852576757ae1456935cad1fd9cf3fb79f53d2bfbb857ce38118cf4bf3deb62eb25db089f05789b44c8dbb33e6c1f3cd7fe09bfde87bfe0e02cd87a1e4b10218994711fc8a70f51b8c151f2fa113f5232af9d85b5e4db2dc5866533a64d3e84eb20393e5a58b7a473f4e0e172be99e1ae9330c27fc5018e50829d0f28497014a41838e398d4bbd01711d184d42c3a243246346461dda097f738784a9ecf162707074429aedc17ec149f28159f029768146995445baca0b2a1e708a7145f92ff15bda7bfef89fe1863fd7d8be3941bcd9cabc739df6c88a245158c821f87476f7b61c72dfae23e6573a82662617dc45e561e3fbb9bdc24ecd3b2cfb938c7849028f43f865ed5ac28fa7c8fa2279c4e6ca82e5f87dbc83620abe0b192aea250459858265126555091b65a5626a0de3014ecd3b50c79fd5d330d034aa6d0e7951bc549fab3d6480cd1f37b3451c7ef7ce47a35bd1e9dbcd1ea56a31762801fddc8c7e5c4fe1812a5408131cd1f501c7f0d23e76f287eae219dfcec81f435b6b711d1bb7582fccde0bd7d780e037cbbf51f52b51eafafdea6e6fe6b78856cb250bf0bd2569df1de87f62fe136791764abe7a7c49617504d805ec839b76d1cc7574498b173116e2b6fa2dd229c5ae306add7d5bff66b70b18c755b838b850c5a838b955097ac0b0fb97e3d55451599a8bc04a489169b92f43e7c72837a928a2a3249790948122d3625a9f027eaa9626ac9849585206d550d53f2529c7ad2680d99acac0024292f3525e73edcb8763d3d451599a0bc04a48816ab48d2f6e78a9d4c0a98c9689d6777536e28cfe3cd2d4ef68bd6fb39ee554430c92af9cbbe04bb676937ae9cc4235d27f1f8f0e1f1f8edc91be41cbff9333e3ef916f792a36d9d545631ed7470e720ebe967e46dfbeeaa953664e6b17f6dc860e7af0d1999e4f317d749ddc265738ba23281d7aa5fc8b3a9ce09948dad0edc30c7ee7c1c1ba0ad2e55d042331a5034d8b570c0f0d6fdde4dbc7a9ffc640881bac4b11db99b7c1261a9d20d52ee42fcaf66ef61146713bd323010d7676452d3ad86a293b2e3dd831f5b0e5c7664852298677db8b21986b629ca6affdf0e897d4d12101cd60a35295cc7dd19a46b82c8f7b3bd36d035f01ca086b0568e720adfbf9f9ca2cedf4d563bb1caaae980da68ff54fbbe82de99f89ac5c4f72f6dbb2169bb729e35756c19b6e8ec7c7fa6d52af329974ade8aa28a89c3721ec7a1ed6654f10b50e96ef2437c17385643e03be732b35c115e1359743744fa0805649225c6c1a8e582c1a0d26b203ce89f245022a6384a9983d213b79828811b24b24cbb81ed6e90573f2aa199a632a46c2f3b104b2ef10607e9e4d58f5ca767660f231350f623e869137b564b4636f444861e4434cdad782aa12130e2a8ee824bece1045be7767e57e902c5367264952462ef68d2a3103545207a50a9e33933a2d0f12cd0e9781602478f999a26583c739a5ae084432e40e068ac775081e33933a2c0f12c98bdc0494118688ae1884c35c955847214a9832f638db4a6423c1941dca0b1eb74cdde309c56e8e8beba7182c58056b3c8c9330cc32ac48646b76a315b98f52adcd064491587f6bdb89f8aa8651b6676b2cdd2d84634cfd2f877c90dcdef52344db170b1626a9f80bfc901b804799c6150a9e3d832a2c471e39fbd3f2004629b261736cc80b0d5d96528d43b985956c42820daea0216b248abc4792035ab09938caf6b309746503898133a9dab03c683685d1eb04a5fab901638a204646763d91396974411722503a251d79846ed44894841d738114f5eabf098e0124b7ac803d0989ddcbc50ec86f652884105a6884318c01697316b61e96eb30196390892d02ab7a801a438269210a8dd32181abd36593bb25c911b409b8094208ce0321225de15666a01d789455d6a0eb296e49702282964734c95012989154d2a3f4483e117b20c8f5e152dd48917b61abb10e36330540ad81b170ad583b9a00a61e904b15a7141083c015c2888eecc05f9ea8acc87fac88a5e6c85190763a06af8018644065109e97cbc860b2a87526bb3df9e07821fc90015f4f6a60e0c65b04600bb7fcdfd7f2bbd90b7fb3aececc008badcc04c50ec613576b1ad06cfef3c019b4009ee8d01b02ad46dad743657ad7830820ea84e7ae5d137edb574775bcc089453a7bb39ea491c8ae3e7d29b2fcb56cb3c3102fdb05a021914563768b371832726a302fd62adf3740a17dfadcd930ef839c692973471ef51f69484117ac24269faa4dec1d99bd94b94a007945e0db8707ca91ab77701fcd1a22b7e7b22cf5de1a716f5d3dfcc0ea9d8bec87b3adae08a0cc44f7785d9dd3c69dd931b5a690a0be4a148719fe322f4b67e006f4fe1d665320216a2fc6880c3a616e0b0d8027d3c36f8cfc2d51d0ac0684c649405ab0998a69a20cc94b4e39684400a87f032a5257190cd699437badf6d217050cb61248e79dbce82309ff5b1aad7ea2c54f5551f893e3f6761e827430ce605b304c694e9a3f28fcc594cbe441f517849ce420a450654b2efc53922d98256780047d535f47b905f88b3e872a98154ca6fc539f1948b5b602b6816cb0ceca3fc9c9cb39372b13e766a5b641dadbecec6f22a0211e666588e149a1b640d8c614cb36a8d841748d015a8de86728e40f5d9108bbefe94c0e8f7590a1018563211a03c26dc4d80000cd8be700f2979f352fbfa13c6e45e477226bcee75288c6726a61309435d14a54906ca48bef9d4c34d873119f48922b774e69ff431b8973e2c1257f04d7afb6090a54944f2a31a73f900da0d231cf2b26fea96b7158d19ac005004d16401c84ebdbad97f35c4b01e407148cda24007d793cd55d7f9e9383766f332961e0e3d177c8052bd6296b16968592c2b408b9fd2c8a5f15620c82484b065ae68cd4906a2be9bc1746d4e15783b448baafa5bb410616228d97832c57306205c054e252dd79d49f559444b96c137db8ce6517db6319e7035cfa1742a205629cd41793a209c02ac6844be39d9b214a2cfab2cac621370b658bfc609f6f7d30afbeb5fbd0bcfcd82d045851b14b88f4498f3e7938ba383c3232179f37c12292fe3d8f114271a7236657eca4678f5e9a63c6d4c08609a0f8acf691c7c4191fd8ca23ff8e8e58f7da42976c8efa49f34c5d9f8a57b6bd781835fce16ffcc9a9d5ad7fff85cb5dcb3ee22227fa7d681f55bf7fcc6056fb488285bf334882f76fb794dad3c081843242b96180d4c8694f2e62a91d37c151dd3e2f685cb65bd5582666fa32bd4f6496e1fdca49704b7a06eb74e62db095191a8b62fbc5e580825a26d830526a15599479dc1aa93d2b6210d4c489b99db8ee968f54d66d132fbbc675dc79f02f7d72d29b827dc102ca7a0597de4a10022f63bbbac7758b93a2f5b0abf404c3ad949d1e5c4920670bd268f2c99f7dbff4052c6ded67a45cec5deb0a7947bf3448bdf86adb967931d02aed0491f59c4d45292e51133b3203be4da2be2283b2b2843f8e1fd89499fd9d2ba2d0c60186ba8346453fb077c7232236af2a66368221cffdfa99d763bf73c1df404ee79cdb9801ed3e1e8ab49da8ae6892afb310b55d1669de2542307ca4c8352ada7cec00a02b3bc6b667034211a2ca75de72c22f4e9f0c849eb4649d7003d2b986f3a10bdac7413a60051bcee9e2e07dd982908ea2ea9ce34e1875ec6b99908130df84c975f6e6c61822eacce4c988cb3c94d993e6eecc5ace6f2e994abd9dcb2c129ef98cc4c74f87d8f8a109d4c45bb2048b537bf0cb774530893ea72103391f2042a27ee9b1423fd291d5d8626b7490d291bfadf591959b5ddf3658c4cd65cf6575ae91667e211e781eec9b22b8eed0f0377c467e60e6b5c9e9d9540712141051943ae4663867a6aae40cf5e8866edd04c2940a3b933c6e233ad47dc32c9e95cf29a4eb9b835bc489ac70a3779e25239479038a7f4f1097f3f1f4e4c9a3f60385b380f2199f5fc94044a5a2781170ba68c5e9428e1b37f854a9956ab39b1a95e5ed3fa6e81048d757dd330676ddfb44e7ddf405a44b1ef6a3722755915a97a82f3cc895dd0f541c2a7df55e0401ab33ac6e516a3966f79957ab6a95385411d3774dad4a15e6742d8009ca7267eeacf18bfb0434ad7d49f5af146cb42cba7c754e73756868ed966eaacca73c8336b3c3cd0c2015954fa1b747f69653b0d9a33ad40e68fee831e288bac32c7e764e20ca5756d4c150bbc35ed65c04a3ba978e9dcfff08bf493cdc3573f6b95b77a0cf14aa2271ab81c41ed2733aeb15e1b49c90c32df76325cac97a1ce58d1df801b155aebed781fabb1c20791dfbaf73e705895b51ea877d4e4b1063d70765fa5b0aa0475149137c8e62bbfcf271beb6d903e16ccffbac4b1fb5441ac0866806d6e4b5dd6b90e1ec362672f50545411ee22dee0043964bf7d1e25ee23b213529cbe13ccfe01e0eced55fa5af5013bd7c1dd36d96c133264ec3f78dcbba4344250d77f96b298a77975975de58ffb180221d34ddf57de053f6e5dcf29e9be525c880420d2d0037d0091ce65923e84787a2d916ec3401388b2af8c98dc637fe311b0f82e58a32fb80d6d44fcdee32764bf560fb52090e689e0d9beba74d15384fc986254edc99f44861dffe5fbff0230b3c9ff399c0000	6.2.0-61023	T
201801050646136_AddSeedTopic	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5ddb6ee4b8117d0f907f10fa2909bc6e5fe2c1c468efc26b8f1363c7f664dab3c8db8096685b585d7a25f58c8d60bf2c0ff9a4fc42288992782b89d4bd27c1028bb6481e168b55c56291acf9cfbffebdfae1c5f7ac2f388add30385b1cee1f2c2c1cd8a1e3064f678b6df2f8dddbc50fdffffe77ab778eff62fd5cd43b4eeb9196417cb6784e92cde97219dbcfd847f1beefda5118878fc9be1dfa4be484cba38383bf2c0f0f9798402c089665ad3e6e83c4f571f607f9f3220c6cbc49b6c8bb091decc5f43b295967a8d62df271bc41363e5bb83f05e1d78575eeb98874bec6dee3c2424110262821a49d7e8af13a89c2e069bd211f9077ffbac1a4de23f2624c493eadaaeb527f709452bfac1a1650f6364e42df10f0f098b26329366fc5d445c92ec2b07784b1c96b3aea8c69678bf320fe8aa3852576757ae1456935cad1fd9cf3fb79f53d2bfbb857ce38118cf4bf3deb62eb25db089f05789b44c8dbb33e6c1f3cd7fe09bfde87bfe0e02cd87a1e4b10218994711fc8a70f51b8c151f2fa113f5232af9d85b5e4db2dc5866533a64d3e84eb20393e5a58b7a473f4e0e172be99e1ae9330c27fc5018e50829d0f28497014a41838e398d4bbd01711d184d42c3a243246346461dda097f738784a9ecf162707074429aedc17ec149f28159f029768146995445baca0b2a1e708a7145f92ff15bda7bfef89fe1863fd7d8be3941bcd9cabc739df6c88a245158c821f87476f7b61c72dfae23e6573a82662617dc45e561e3fbb9bdc24ecd3b2cfb938c7849028f43f865ed5ac28fa7c8fa2279c4e6ca82e5f87dbc83620abe0b192aea250459858265126555091b65a5626a0de3014ecd3b50c79fd5d330d034aa6d0e7951bc549fab3d6480cd1f37b3451c7ef7ce47a35bd1e9dbcd1ea56a31762801fddc8c7e5c4fe1812a5408131cd1f501c7f0d23e76f287eae219dfcec81f435b6b711d1bb7582fccde0bd7d780e037cbbf51f52b51eafafdea6e6fe6b78856cb250bf0bd2569df1de87f62fe136791764abe7a7c49617504d805ec839b76d1cc7574498b173116e2b6fa2dd229c5ae306add7d5bff66b70b18c755b838b850c5a838b955097ac0b0fb97e3d55451599a8bc04a489169b92f43e7c72837a928a2a3249790948122d3625a9f027eaa9626ac9849585206d550d53f2529c7ad2680d99acac0024292f3525e73edcb8763d3d451599a0bc04a48816ab48d2f6e78a9d4c0a98c9689d6777536e28cfe3cd2d4ef68bd6fb39ee554430c92af9cbbe04bb676937ae9cc4235d27f1f8f0e1f1f8edc91be41cbff9333e3ef916f792a36d9d545631ed7470e720ebe967e46dfbeeaa953664e6b17f6dc860e7af0d1999e4f317d749ddc265738ba23281d7aa5fc8b3a9ce09948dad0edc30c7ee7c1c1ba0ad2e55d042331a5034d8b570c0f0d6fdde4dbc7a9ffc640881bac4b11db99b7c1261a9d20d52ee42fcaf66ef61146713bd323010d7676452d3ad86a293b2e3dd831f5b0e5c7664852298677db8b21986b629ca6affdf0e897d4d12101cd60a35295cc7dd19a46b82c8f7b3bd36d035f01ca086b0568e720adfbf9f9ca2cedf4d563bb1caaae980da68ff54fbbe82de99f89ac5c4f72f6dbb2169bb729e35756c19b6e8ec7c7fa6d52af329974ade8aa28a89c3721ec7a1ed6654f10b50e96ef2437c17385643e03be732b35c115e1359743744fa0805649225c6c1a8e582c1a0d26b203ce89f245022a6384a9983d213b79828811b24b24cbb81ed6e90573f2aa199a632a46c2f3b104b2ef10607e9e4d58f5ca767660f231350f623e869137b564b4636f444861e4434cdad782aa12130e2a8ee824bece1045be7767e57e902c5367264952462ef68d2a3103545207a50a9e33933a2d0f12cd0e9781602478f999a26583c739a5ae084432e40e068ac775081e33933a2c0f12c98bdc0494118688ae1884c35c955847214a9832f638db4a6423c1941dca0b1eb74cdde309c56e8e8beba7182c58056b3c8c9330cc32ac48646b76a315b98f52adcd064491587f6bdb89f8aa8651b6676b2cdd2d84634cfd2f877c90dcdef52344db170b1626a9f80bfc901b804799c6150a9e3d832a2c471e39fbd3f2004629b261736cc80b0d5d96528d43b985956c42820daea0216b248abc4792035ab09938caf6b309746503898133a9dab03c683685d1eb04a5fab901638a204646763d91396974411722503a251d79846ed44894841d738114f5eabf098e0124b7ac803d0989ddcbc50ec86f652884105a6884318c01697316b61e96eb30196390892d02ab7a801a438269210a8dd32181abd36593bb25c911b409b8094208ce0321225de15666a01d789455d6a0eb296e49702282964734c95012989154d2a3f4483e117b20c8f5e152dd48917b61abb10e36330540ad81b170ad583b9a00a61e904b15a7141083c015c2888eecc05f9ea8acc87fac88a5e6c85190763a06af8018644065109e97cbc860b2a87526bb3df9e07821fc90015f4f6a60e0c65b04600bb7fcdfd7f2bbd90b7fb3aececc008badcc04c50ec613576b1ad06cfef3c019b4009ee8d01b02ad46dad743657ad7830820ea84e7ae5d137edb574775bcc089453a7bb39ea491c8ae3e7d29b2fcb56cb3c3102fdb05a021914563768b371832726a302fd62adf3740a17dfadcd930ef839c692973471ef51f69484117ac24269faa4dec1d99bd94b94a007945e0db8707ca91ab77701fcd1a22b7e7b22cf5de1a716f5d3dfcc0ea9d8bec87b3adae08a0cc44f7785d9dd3c69dd931b5a690a0be4a148719fe322f4b67e006f4fe1d665320216a2fc6880c3a616e0b0d8027d3c36f8cfc2d51d0ac0684c649405ab0998a69a20cc94b4e39684400a87f032a5257190cd699437badf6d217050cb61248e79dbce82309ff5b1aad7ea2c54f5551f893e3f6761e827430ce605b304c694e9a3f28fcc594cbe441f517849ce420a450654b2efc53922d98256780047d535f47b905f88b3e872a98154ca6fc539f1948b5b602b6816cb0ceca3fc9c9cb39372b13e766a5b641dadbecec6f22a0211e666588e149a1b640d8c614cb36a8d841748d015a8de86728e40f5d9108bbefe94c0e8f7590a1018563211a03c26dc4d80000cd8be700f2979f352fbfa13c6e45e477226bcee75288c6726a61309435d14a54906ca48bef9d4c34d873119f48922b774e69ff431b8973e2c1257f04d7afb6090a54944f2a31a73f900da0d231cf2b26fea96b7158d19ac005004d16401c84ebdbad97f35c4b01e407148cda24007d793cd55d7f9e9383766f332961e0e3d177c8052bd6296b16968592c2b408b9fd2c8a5f15620c82484b065ae68cd4906a2be9bc1746d4e15783b448baafa5bb410616228d97832c57306205c054e252dd79d49f559444b96c137db8ce6517db6319e7035cfa1742a205629cd41793a209c02ac6844be39d9b214a2cfab2cac621370b658bfc609f6f7d30afbeb5fbd0bcfcd82d045851b14b88f4498f3e7938ba383c3232179f37c12292fe3d8f114271a7236657eca4678f5e9a63c6d4c08609a0f8acf691c7c4191fd8ca23ff8e8e58f7da42976c8efa49f34c5d9f8a57b6bd781835fce16ffcc9a9d5ad7fff85cb5dcb3ee22227fa7d681f55bf7fcc6056fb488285bf334882f76fb794dad3c081843242b96180d4c8694f2e62a91d37c151dd3e2f685cb65bd5582666fa32bd4f6496e1fdca49704b7a06eb74e62db095191a8b62fbc5e580825a26d830526a15599479dc1aa93d2b6210d4c489b99db8ee968f54d66d132fbbc675dc79f02f7d72d29b827dc102ca7a0597de4a10022f63bbbac7758b93a2f5b0abf404c3ad949d1e5c4920670bd268f2c99f7dbff4052c6ded67a45cec5deb0a7947bf3448bdf86adb967931d02aed0491f59c4d45292e51133b3203be4da2be2283b2b2843f8e1fd89499fd9d2ba2d0c60186ba8346453fb077c7232236af2a66368221cffdfa99d763bf73c1df404ee79cdb9801ed3e1e8ab49da8ae6892afb310b55d1669de2542307ca4c8352ada7cec00a02b3bc6b667034211a2ca75de72c22f4e9f0c849eb4649d7003d2b986f3a10bdac7413a60051bcee9e2e07dd982908ea2ea9ce34e1875ec6b99908130df84c975f6e6c61822eacce4c988cb3c94d993e6eecc5ace6f2e994abd9dcb2c129ef98cc4c74f87d8f8a109d4c45bb2048b537bf0cb774530893ea72103391f2042a27ee9b1423fd291d5d8626b7490d291bfadf591959b5ddf3658c4cd65cf6575ae91667e211e781eec9b22b8eed0f0377c467e60e6b5c9e9d9540712141051943ae4663867a6aae40cf5e8866edd04c2940a3b933c6e233ad47dc32c9e95cf29a4eb9b835bc489ac70a3779e25239479038a7f4f1097f3f1f4e4c9a3f60385b380f2199f5fc94044a5a2781170ba68c5e9428e1b37f854a9956ab39b1a95e5ed3fa6e81048d757dd330676ddfb44e7ddf405a44b1ef6a3722755915a97a82f3cc895dd0f541c2a7df55e0401ab33ac6e516a3966f79957ab6a95385411d3774dad4a15e6742d8009ca7267eeacf18bfb0434ad7d49f5af146cb42cba7c754e73756868ed966eaacca73c8336b3c3cd0c2015954fa1b747f69653b0d9a33ad40e68fee831e288bac32c7e764e20ca5756d4c150bbc35ed65c04a3ba978e9dcfff08bf493cdc3573f6b95b77a0cf14aa2271ab81c41ed2733aeb15e1b49c90c32df76325cac97a1ce58d1df801b155aebed781fabb1c20791dfbaf73e705895b51ea877d4e4b1063d70765fa5b0aa0475149137c8e62bbfcf271beb6d903e16ccffbac4b1fb5441ac0866806d6e4b5dd6b90e1ec362672f50545411ee22dee0043964bf7d1e25ee23b213529cbe13ccfe01e0eced55fa5af5013bd7c1dd36d96c133264ec3f78dcbba4344250d77f96b298a77975975de58ffb180221d34ddf57de053f6e5dcf29e9be525c880420d2d0037d0091ce65923e84787a2d916ec3401388b2af8c98dc637fe311b0f82e58a32fb80d6d44fcdee32764bf560fb52090e689e0d9beba74d15384fc986254edc99f44861dffe5fbff0230b3c9ff399c0000	6.2.0-61023	T
201801060316460_AddUpdatedDateToAnswer	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5ddb6ee4b8117d0f907f10fa2909bc6e5fe2c1c468efc2eb4b62ecf89269cf226f035aa2dbc2aaa55e493d6323d82fcb433e29bf104aa224de4a2275ef4930c0c016c9c362b1aa582c92e5fffcebdf8b1f5ed79ef50587911bf867b3c3fd8399857d3b705c7f7536dbc6cfdfbd9ffdf0fdef7fb7b872d6afd6cf79bde3a41e69e94767b39738de9ccee791fd82d728da5fbb761844c173bc6f07eb397282f9d1c1c15fe68787734c206604cbb2161fb77eecae71fa0bf9f522f06dbc89b7c8bb0d1cec45f43b2959a6a8d61d5ae368836c7c36737ff283af33ebdc7311e97c89bde799857c3f88514c483bfd14e1651c06fe6ab9211f90f7f8b6c1a4de33f2224c493e2dabeb527f7094503f2f1be650f6368a83b521e0e13165c75c6cde88a9b3825d84615784b1f15b32ea946967b3733ffa8ac3992576757ae1854935cad1fd8cf3fb59f53d2bfdb857cc38118ce4df9e75b1f5e26d88cf7cbc8d43e4ed590fdb27cfb57fc26f8fc12fd83ff3b79ec71244482265dc07f2e9210c36388cdf3ee2674ae68d33b3e67cbbb9d8b068c6b4c98670e3c7c74733eb8e748e9e3c5ccc3733dc651c84f8afd8c7218ab1f380e218877e8281538e49bd0b7d11118d49cdbc432263444366d62d7afd80fd55fc72363b3938204a71edbe6227ff44a9f8e4bb44a348ab38dc620595353d8738a1f892fc97f79efcfc48f4c718ebd3c681b1aa9bfe7d8ba38491f54cafc639df6c888e86258c82958747ef3be1e41dfae2aed2e9571331b33e622f2d8f5edc4d664df669d9e74c1322424818ac3f065ed92c2ffafc88c2154e642250972f836d681b9095f35849575ea8224c2c9328932aa8485bcc4beb516d5372f6e91a95acfeae59951e2553e8f3da0da338f9b1d2bef4d1f3073452c7576be47a15bd1e9dbcd3ea56a31762bb9fdd708d8b89fd31204a817c639a1f50147d0d42e76f287aa9209dfcd801e94b6c6f43a277cb18ad37bdf7f6f012f8f86ebb7e4ad47ab8be3a9b9ac7afc135b2c91a7fe527ad5ae37d08ec5f826d7ce5a78be5a7d8365d2f0b804ec839b76d1c45d74498b173116c4b47a4d9229c58e31aadd7d5bfe66b70be8cb55b83f3850c5a83f3955097ac0b0fb9eb6aaaf22a3251590948132d3625e943b072fd6a92f22a3249590948122d362529f727aaa9626ac9841585206d650d53f2129c6ad2680d99acb40024292b3525e731d8b876353d791599a0ac04a48816ab48d2f6e7f24d500298ca689567775bec45cfa3cd1d8ef7f3d6fb19ee754830c92af9cbbe04bb6769372e9dc4235d27f1f8f0e9f9f8fdc93be41cbffb333e3ef916b7a1836d9d545631e9b477e720ede967e46dbbeeaa9136a4e6b17b6d4861a7af0d2999e4f317d749dcc2797d8bbc3281d7aa9fcbb3a9ce09940dad0edc3087ee7c181ba0ad2e65d042331a9037d8b57040ffd6fdd18dbd6a9ffca40f81bac4911dba9b6c1261a9d28d6fee42fcaf62ef61146713bd323010d7656452d3ad86a293b2e3dd811f5b0c5c7664852298675db8b22986b6294a6bffdf0e897d8d1210ecd70ad5295ccbdd19a46b82c877b3bd36d035f01ca082b0468e7202dfbd9f9ca04edf4d563bb1caaac9809a68ff58fbbe9cde89f89af9c4772f6dbb2169bb729e35766c19b6e8ec7c7fa6d54af329974ade8aa28a89c3721e4581eda654f10b50e16ef243bcf21dab26f09d719959ae08af892cba1b227d840232c912e360d462c16050e90d121ef44f122811531c26cc41c9895b4494c0f56359a65ddf7637c8ab1e95d04c531912b6171d8825977883fd64f2aa47aed333b387910928fa11f4b48e3d8b39231b7a22430f22eae6563c95d010187154f7fe25f6708cad733bbbe67481221b39b24a12b17734e951889a2210ddabd4f19c1950e87816e8743c0981a3c74c75132c9e398d2d70c22117207034d6dbabc0f19c1950e078164c5ee0a4200c34c57044a69ce432423988d4c197b1065a53219e0c206ed0d875ba666f188e2b74745f5d3bc16240ab5ee4e4198661156243a35b95980dcc7a196ea8b3a48a43fb4edc4f45d4b209335bd966696c039a6769fcbbe486667729eaa658b85831b64fc0dfe4005c822cced0abd4716c1950e2b8f14fde1f1002b175930b1b6640d8aaec3214eaedcd2c2b6214106d55010b59a455e2dc939a55844986d735984b03281ccc099dced501e35eb42e0b58250f5d480b1c5202d2b3b1f4f5cb6bac08b99201d1a86b44a376a24424a04b1c8b27af65784c7089253de40168cc4e6e9e2b764d7b29c4a00253c4210c60f3cb9895b074b75903cb1c044968a55b5403921f134908d46e190c8d5e9bac1c59a6c835a075404a104670198912ef0a33b580ebc4a22ed507590bf20b019414b23ea6ca8014c48a26951fa2c1f073598647af8a16eac40b1b8d5d88f131182a05ec8c0bb9eac15c5085b07482588db820049e002ee444b7e6827c7545e6437564452fb6c28c83315015fc004322bda884743e5ec1059543a9b5d96fce03c18f6480727a3b53078632582380ddbfe6febf915ec8db7d1d76b660045d6e602628f6b01abbd84683e7779e804da00477c6005815aab6563a9bab463c1840075427bdf2e8ebf65ababb2d6604caa9d3dd1c75240ef9f173e1cd17658b799653817e58cc81e40b8b5bb4d9b8fe8a49c640bf58cb2c13c3c5774bf37c05eb0c63ce4b9ab8f7287a8a8310adb0509abcc67770fa66f612c5e8092557032e9cb5548ddbbb00fe68de15bf3d91e72ef753f3fac9cfcc0e29dfbec87b3adae09a0c649dec0ad3bb79d2ba2737b492ec17c843a1e23ec745e06dd73ebc3d855b17790c5888e2a3010e9b9580c3620bf4f1b8cc042c1e57a08fc71e26b07055870c301a136965c12a02b0896609332fede025a192c22bbc8c6a493064c36ae597ee9f1b0830d4b21f0966decab320cc677dacf2f53b0b557ed547a2cfd95918fac9108379112d813165faa8fca37516932fd147145ea6b39042910195ecfb738e48b6a0111ec051750dfd1ee417e72cba5c6a2095f2db734e3ce5e206d80a9ac53203fb283f4fe7eca45c6cb03244f95b756e5928be4ec6f22a021be666588e3c9a1b640d8c7e4cb36a8d841748d0b528df9a728e45f9d9108bbe2695c0e8f7490a1018a63211a02cc6dc4e80000cd8be700f3379f352f99a14c6e45e5b7226bceab5298c6726a62309435554a64e068a9301f3a9879bf66332e893476ee9cc3ee963702f875824aee09bf4f6c1a04d9d8864473fe6f201b4eb4738e465dfd42d6f2a1a135801a088a4c902909ea2b5b3ff6a887e3d80fcd09b45810ec2479babb6f3d3726ecce665283dec7b2ef880a77ac52c62ddd0b2585480163fa5914be2b74090490889cb5cd19a9314447dd783e9da9c2af0b6891655d5b77221c2c4d0b4f1648ae71640b80a9c4a5aae3b93eab38d862c836fca19cda3faac6438e1aa9f43e99441ac529883e2b441385558d0087f7dde6729e49f559959f926e06cb67c8b62bcde4f2aec2f7ff52e3c370d6ae7156e91ef3e1361ce9e63ce8e0e0e8f843cd2d3c9e93c8f22c7539c90c8899df9291be015a99bf0b436c180697e293ebdb2ff0585f60b0affb046af7fec2263727286107794315985a593f944cea09cb252ba5277e33bf8f56cf6cfb4d9a975f38fcf65cb3deb3e24a27c6a1d58bfb54fbd9cb3598b88a2354f83f898b89b87deca338521a4bb6489d1c0644829a5af123949a5d132636f57b85c425e2568fa6cdb4ce6d5f9779fdcb893dcbba099689c5fb715a222876e57789db010ca91db040bcc8fdbd43aaaf3e536210dcc959b9adb969972f54d66de32fdbc67dd449f7cf7d72d297824dc102ca7a0595da4c80082ff3beb21b458b95a2f5b0a1743cc87d94ad1e59c9706709de6b52c98f7dbff40bec8ced6fa07391d6467d863cabd790ec86fc3d63cb279180157e8a48b04676a2949539c9959901d72ed1521999d15943efcf0eec4a4cb446eed16063022d65786b4b1fd033e6f9a113559d32134113e4ad8a99d7633f73c19f408ee79c511831ed3e140ae49468dfa892afa310b55d166ade2540307ca4c83528da7cec00a02b3bc6b66703021ea2ddd5eeb0427f455f3c0f9f406c92401bd78986ea612bd8479236627513c3c1f2f3dde90d911aaeebb4e3417895e32bc8908130df88c97fa6e686182eebe4e4c988c13dd8d99d96ee8c5ace21eeb98abd9d412d529afab4c4c74f87d8f8a109d244abb20489597c80cb774630893ea9e113391f2042a27ee9b1423fd291d5c8646b74935d924badf591959b5ddf3658c4cd654f6575a992027e2116781eed1123f0eed0f03d7cd27e60e6bdcc39d944071214105197dae4643867a2a6e534f5e8826edd08c294083b933c6e233ae47dc30ffea5452ae8eb9b8d53c6e9ac60a377a4e55397d9138a7f41d0b7fd51fce999abd85389b394f0199f5ec9404caa72781e70ba68c9e9728e1d33f90a5ccf8559f73552fe56a75b740eec8aabe6998b3b26f5aa7ba6f2063a3d877b91b91ba2c8b543dc129f0c42ee8fa20e1d3ef2a7020c35a15e3328b51c9b7ac4a35dbd459cca08e6b3aadeb50af33216c00ce531d3ff5678c5fd821a5abeb4fad788325c8e53377aa532f2b43c76c3375c2e729a4c0351e1e68e180842cdd0dbabb8cb7ad06cd9956208948fb41f794e056997e7434718632ced666b1059ead763260a59d543c9aee7ef87966ccfae1ab5fc8ca5b3d867825d1230d5c8ea07693b4d758af8da4640249795b192ed6cb5027bfe86ec0b50aadf50cbd8bd558e183c8cfe63b1f38acca5a6fdd5b6af25083ee39f1b0525855823a88c81b241a969ffa938df5d64f1e0b66bf5de2c85d95100b82e9639bdb5217756efce720dfd90b14e55584bb88b738460ed96f9f87b1fb8cec981427ef04d3bf4d9cbebd4a5eab3e61e7c6bfdfc69b6d4c868cd74f1ef72e29891054f59f6653e6695edca757f9a32e8640c87493f795f7fe8f5bd7730abaaf151722018824f4401f40247319270f21566f05d25de06b0251f615119347bcde78042cbaf797e80b6e421b11bf0f7885ecb7f2a11604523f113cdb17972e5a85681d518cb23df995c8b0b37efdfebf57714f890f9d0000	6.2.0-61023	T
201801062330581_AddSomePropertiesToAppUser	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5d5b6fdcba117e2fd0ff20ec535bf8787d6982d4589f03c797d638f1a559e7a06f812cd16b215a698fa44d6c14e797f5a13fa97fa194444abc8d44eabe691120b04572381cce0c8743f2f37ffef5efc54f2f6bdffa8aa2d80b83d3d9e1fec1cc428113ba5eb03a9d6d93a71fdecd7efaf1f7bf5b5cbaeb17eb175aef38ad875b06f1e9ec39493627f379ec3ca3b51defaf3d270ae3f029d977c2f5dc76c3f9d1c1c15fe68787738449cc302dcb5a7cdc0689b746d92ff8d7f33070d026d9dafe4de8223f26df71c932a36addda6b146f6c079dcebc9f83f0dbcc3af33d1b77be44fed3ccb283204cec04b376f22946cb240a83d572833fd8fec3eb06e17a4fb61f23c2f249595d97fb83a394fb79d9909272b67112ae0d091e1e1371ccc5e68d843a2bc485057689059bbca6a3ce84763a3b0be26f289a59625727e77e94562312ddcf25bf9f57dfb3b28f7bc58c63c548ffed59e75b3fd946e83440db24b2fd3deb7efbe87bcecfe8f521fc8282d360ebfb2c4398255cc67dc09feea37083a2e4f5237a226c5ebb336bceb79b8b0d8b664c9b7c08d741727c34b36e71e7f6a38f8af96686bb4cc208fd150528b213e4dedb4982a220a581328949bd0b7d61154d704dda21d6316c2133ebc67ef9808255f27c3a7b7370808de2ca7b412efd44b8f81478d8a270ab24da220597353d4728e5f802ff477b4f7f7ec0f6634cebd3c685695537fdfb16c5a920eb855e4de76cb3c1361a956414a23c3c7ad789246fedafde2a9b7e351333eb23f2b3f2f8d9dbe4de649f947dce2d21c68c44e1fa63e897cd68d1e7073b5aa154274275f932dc468e015b54c64abe68a18a31b14ce24caaa0626d312fbd47b54fa1e2d3752a79fd5df32a3d6aa6d0e79517c549fa63a57fe9a3e70ff6481d63ef118515bd1ebd79d347b778057053cdcdfb7dff9a341059e890c57a60915dae6dcfaf14d95bad6e357ac1cbdd9317ad51610bef43ec47ecc098e77b3b8ebf8591fb373b7eae601dffd801eb4be46c23ecaa9689bddef4dedbfd7318a0dbedfab1d4a721faea6c6a1ebe8557b683c3a2cb206dd59a1eb68b2fe136b90cb2f8e253e298861805814ed839731c14c7575899917b1e6ecbd8ad59dc922e60358e52d7fe9a872d74e56f17b6d0b51f0a5b68f0a0cbd6b96f7beb6aae681599a9bc04e489149bb2f4215c7941354bb48acc525e02b2448a4d59a2215835574c2d99b1a210e4adac61ca5e4aa79a355243662b2b0059ca4b4dd97908379e53cd0fad22339497801c9162154bda2130dd37a604331dad0a866f8aedfb59bcb945c93e6dbd9fd3bd8a304dbc4a7ed997c8ee59da8dcbb8fa4837ae3e3e7c7c3a7ef7e6aded1ebffd333a7ef33deedc07db6daabc62da69efc141d6d32fb6bfedbaab46d690b9c7eead21233b7d6bc8d8c49fbf7ad936635edf8256c6e4b5ea537d36b53981b3a1cd811be6d09d0fe303b4cda5ccf368265068835dcba0f4efdd1fbcc4af8ec97b49235ca0d889bc4d4d1ae0503725bc0b29d38abd87516a528ccac0dc6597c95ccdb01a4ae8ca817707716c31703990158a60997511ca6634b45d5156fbff7e48ec6b941c6abf5ea8cee05aeece205b1354be9bedb581ad814727158c350a9453f2ddc7c929d5e987c9ea205659351d5013eb1f6bdf47f99d48ac4927be7b6ddb0d4ddb9523c0b173cbb04767e7fb33a956ba4fb9548a5614554c0296b3380e1d2fe38a5f808a70931fe265e05a3589ef5ccacc7285658d75d1db60edc31ce049960407532d160c862ab974c313fd934414ab298a52e1d8e9895b8c8dc00b1259a7bdc0f136b65f3d2aa199a631a4622f3a104b2ed0263d2e0d92ea91ebf4ccec6164068a7e043bad13cf62cee8869eca908388bab9154f253414461cd55d70817c9420ebccc96f869ddbb163bbb24962b57735f951a89a2211ddabd6f1921950e97811e8743c098523c74c75132c9e398dad70c22117a07024d7dbabc2f1921950e178114c5ee1a4240c34c57046a69ce432433988d6c1f7d7065a5321990ca06ed0d875ba662f658eab74645f5d3bc16242ab5ee5e41986c92ad48664b72a693670eb65baa1ce932a0eed3b093f1559cb26c26ce59ba5b10de89ea5f1ef52189adfa5a89b62e162c5d831017f93030809f23c43af5ac78965408de3c63ff9784048c4d64d2eec980165abf2cb50aab737b7acc85140bc55252c649556a9734f6656912619ded660290d6070b024743a57278c7bb1ba3c6195be0dc22d504418c8cec6b207432f8922e58a0744b2ae31c9da891a91125da2443c792dd36342482cd9214f80e4ece4e6d4b06bda4b29061531451ec2802cbd8c594996ec366bc832074112b5322caa21428f89240ac46f190c8d5c9bac1c596ec83544eb082989308acb6894785798a9055c27166da93ec95ab05f28a06490f539558648c1ace852f9211a0c9fea323c7a55b650275fd868ec428e8fa1a132c0cea4404d0f96822a85a593c46a240521f104488132dd5a0af2d515590ed59915bddc0a330ec64155c8034c89f46212d2f97885145401a5d666bfb90c8438922144f9edcc1c18ce608b0076ff9afbff4676216ff775c4d9421064b98185a0d8c36aec621b0d9edf79023e8130dc99006053a8da5ae96cae1ac960001b509df4caa3afdb6be9eeb6981128a74e7773d4913ad0e3e7229a2fca16f31c86827c58cc01bc8ac58dbdd978c18ac1af205fac650e5e71fec3d21ce2619dd398f39a26ee3d8a9e9230b25748284d010c5c943d33beb013fbd14eaf069cbb6ba91ab77701e251da15bf3d91e78ec6a9b47efa33b343a2db17794f471a5ce181acd35d6176374f5af7e486560a1862fb76a4b8cf711efadb75006f4fe1d605f4034ba2f86840870572e068b105faf4383007961e57a04f8f3d4c60c9551d32c0d4984c2b4bac22019b5a9630f3d20e5e522a29bdc2eba89606433eac567fc9feb98102432dfbd160065e8025c27cd6a7550206b0a4caaffa9408020037acfc933e0dfa9c9f2542bf198ca978d1cf8da9f8aa4f893cd167c9904f86349857de1231a64c9f2aff109fa5c997e853145edbb3248522032ed937f51c936c41237a8044d535f47b905fd1b3d4e55223ad14dfd30bea291637a0ade0592c33f0f9f2937bcef7cbc506ab5d4cdfdf734b5df17532ab89225963beb4c8d954f3454683463fcb8d6add87177d305c2adfcf72c152f9d9901679212b1123df27a94060eacd4481f2bc793b050268c0fe857b6ccabb97ca17b2304dee0529e7c2ab5ed0c2f4ccd4742465a8ca34d5e94071da613ef570d37e5c0679c6c92d9df9277d1adc6b28961257f05dee60c044549d8ae4c759e6fa01b4eb4739e465df74abd1543526b0024059569305203b196ce7ffd524fa8d00e8413e4b053adc1f6daedace4fcbb9319b97a1ecb0efb9e093b8ea15b3c8df43cb6251015afc944e2ecd4903893321cd2f4b456b4e3222eafb2b4cd7e65c813768b4b8aabe690c3126a6db8d27533c8b015270e0549272dd99549fd73414197cfbcf681ed5e73fc32957fd1c4a27276295c21d142728c249c9829c5ad4c37f4bc718799599453701a7b3e56b9ca0f57e5a617ff9ab7fee7b59a29e56b8b103ef092b73fec4747674707824c0894f07da7b1ec7aeaf38f591f1bdf9291be065ac97cab41634c114338b47d90ebeda91f36c477f58db2f7fec02383b3d17493a02ce56d1d241739181b433514ad704af0317bd9ccefe99353bb1aefff1b96cb967dd4558954fac03ebb7f608dc54cc5a4c14ad791ec407d2dd3c5e579e930ca1dda5488c06269394909d959453789096c0cd5dd1e5709995443348a556b0cb8917bc664adf1279b9ab3173c0cac098df9adab91a47f9d1331fb60a4319748d8d71925b515460217745af13114258c74d688138c74d570435ee7113d640cce326d6569ead982e13b465f679cfba8e3f05deaf5b5cf080a521ac168265750175021c78ec6c54d462b56ebd542bc22a11d7b495a1cbd8a506e43ac5272d84f7dbff00ee6767f1cdbd0cebd919ed31f5de1ccbf3fbf0350f2c9e6667e19f02a84ead2519549d9907d9a1ed8c220db5b38ad2c7dea33b35e91290afddc2006601751f2e9a22dd8d1d1ff0f87746dce44d87b044f8f864a7b20bcdc2f374d02384e715c72a7a428793d726c828f51355f463969e23cd5ae5e6064e0e9a26e21a4f9d8117046679d7dce0604ad41b6c626ba01af23a7d605cc4411041a0972b724f53419cd1033e1c1165460120301ecce190281755777c0d3ce9f4400d27a24c24e1331e84e1d0ca04ddf79d98321903168e895038f46256717777ccd56c6a8083ca2b3a13531d7edfa36244070c6b1714a9f2e29ce1966e0c6552ddad6226529e40e5c47d976aa43fa583ebd0e83ea90615a4fb9d959157dbbd58c6c8654d657fa585e8399188384f748f06e039743c0c5cb19f5838ac71f778520ac5a504156cf4b91a0d99eaa9b8413e79259a744033a6020d16ce18abcfb81171431cdda940e78eb9b8d53ce89ac60a373a36ae0c4325ce2979bbc33f6f80b16ff3f71fa733f731bdb59c9f9240b8881271ba60cad46989927cf687ce94c86df5d8b97ad0b9d5dd0218a0557d93346765dfa44e75df00f2a6d877b91b91ba2c8b543dc150866217647d90e893ef2ae200525e95e0728f5129b7bc4ab5d8d4687450c7359dd675a8d799903600e7a94e9efa33c62fec90d1d5f5a736bcc1808e7904563584b63275cc365303774f01cad87878a087034068ba1b7477c8c5ad06cdb9560038a5fda07b022a56c2c88ea6ce1072702d1a31f054b793012bfda4e2a178f7c3a708a7f5c357bf0a96b77a0cf34aa6471ab89c41ed067cd9d8ae8db46402e0caad1c171b65a8013fba1b70ad416b3dbdef623556c420325440e703874d59eb7d7f4b4b1e6ad03d03482b9555a5a883a8bc0160b40c6f8037d6db207d2c98ff7681626f559258609a0172b82d7551e73a780ae9ce5ee0885611ee22dea0c476f17efb2c4abc27db497071fa4e30fb1bd3d9dbabf4b5ea2372af83bb6db2d92678c868fde873ef92d20c4155ff192a36cff3e22ebbca1f773104cca697beafbc0bde6f3ddf2df8be525c880448a4a907f200229dcb247d08b17a2d28dd8681262122be2263f280d61b1f138bef82a5fd1535e10dabdf07b4b29dd7f2a11644a47e2278b12f2e3c7b15d9eb98d028dbe35fb10ebbeb971fff0b91e25d0d0aa00000	6.2.0-61023	T
201801070104163_ModifySomePropertiesInAppUser	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5d5b6fdcba117e2fd0ff20ec535bf8787d6982d4589f03c797d638f1a559e7a06f012dd16b215a698fa44d6c14e797f5a13fa97fa194444abc8bd47dd32240608be470389c190e87e4e7fffcebdf8b9f5ed681f315c6891f85a7b3c3fd8399034337f2fc70753adba64f3fbc9bfdf4e3ef7fb7b8f4d62fce2fa4de71560fb50c93d3d9739a6e4ee6f3c47d866b90ecaf7d378e92e829dd77a3f51c78d1fce8e0e02ff3c3c3394424668896e32c3e6ec3d45fc3fc17f4eb7914ba70936e417013793048f07754b2cca93ab7600d930d70e1e9ccff398cbecd9cb3c007a8f3250c9e660e08c328052962ede4530297691c85abe5067d00c1c3eb06a27a4f20482066f9a4aa6ecafdc151c6fdbc6a4848b9db248dd696040f8fb138e67cf346429d95e24202bb44824d5fb351e7423b9d9d85c93718cf1cbeab93f320ceaa6189ee1792df2faaef39f9c7bd72c6916264fff69cf36d906e63781ac26d1a8360cfb9df3e06befb337c7d88bec0f034dc0601cd10620995311fd0a7fb38dac0387dfd089f309bd7decc99b3ede67cc3b219d5a618c275981e1fcd9c5bd439780c6039dfd470976914c3bfc210c62085de3d48531887190d984b4ce89deb0ba9688a6a920e918e210b993937e0e5030c57e9f3e9eccdc101328a2bff057ae413e6e253e8238b42add2780b255cd6f41cc38ce30bf41fe93dfbf901d98f35ad4f1b4f4d4bdff4ef5b986482ac17ba9eced966836c34aec848447978f4ae1349de82affe2a9f7e391333e7230cf2f2e4d9df14de641f977d2e2c21418cc4d1fa631454cd48d1e70710af60a61391bc7c196d63d7822d2263295fa450c6185f2670265490b1b69857de43ef5388f84c9d4a517fd7bc4a8f9ac9f579e5c7499afda8f52f7df4fc018cd431f21e71a4e9f5e8cd1ba36ef5bd2087ef658a5a74f3fe356d20a1c8c56b735b09e9fbb95c033fd00ae46d57bda0c5ecc98fd7b0d4f4f711f21220b496cd3d48926f51ecfd0d24cf1ad6d18f1db0be84ee36468e689982f5a6f7deee9fa310de6ed78f95fa0cd1576753f3f02dba022e0a7a2ec3ac556b7ac80cbe44dbf432cca3874fa96b1b4094043a61e7cc7561925c216586de79b4ad22b3665149b63cd5b84153fb6b1e949075bd5d50425676555042420353b6ce03e0aff55c912a2253458992275c6ccbd28768e5877a96481591a5a244c9122eb6658904587aaea85a226365a192b7aa862d7b191d3d6bb886c8565ea064a928b565e721daf8ae9e1f524564a8285172848b652c1907b864579811cc755417eade949bf3b364730bd37dd27abfa07b15239a6895fcb22f90dd738c1b5751f39169d47c7cf8f874fceecd5be01dbffd333c7ef33deecb07db4bcabc62d669efc141ded32f20d876dd55236bc8dd63f7d690939dbe35e46ca2cf5ffd7c5731af6f412a23f246f5893edbda1cc7d9d0e6c00c73e8ce87f101c6e65265710cd323a4c1aee547faf7ee0f7e1ae86372b32481a5425dc0c48dfd4dcdaeffd034e1bb0b0951cddec32af1c84765cacc6497a95ac3b05a95ae1503ef0ee2d872e06220cb15a965d645289bd330764579edfffb21beaf5132a4fd7aa13a836bb93b53d91aa7f2dd6caf2d6c4d7930a261ac51a09c91ef3e4ecea84e3f4c9607b1d2aad9809a58ff58fb3ec2ef44624d32f1dd6bdb6e68daae1cf08d9d5b567b747abe3fe36a95fb144b85684552c52660394b92c8f573aed805a80c37d9215e869e5393f82ea44c2d5748d64817fd0dd23ec4019a6441706aaae5824151c5576a58a27f128822358571261c909db825c808fc301575da0f5d7f0302fda8b86686c69089bdec802fb9809bec74344cf52337e999dac3880c94fd70765a279ec59cd20d3395c107117573cb9f4a18280c3faabbf002063085ce995bdcfb3a07890b3cd12491da7b86fc48544d9288ee55eb58c90ca874ac084c3a9e84c2e163a6ba09e6cf9cc65638ee904ba17038d7dbabc2b1921950e158114c5ee184248c6a8ad519996a92ab0ce5205aa7be9d36d09aaa92c900eaa61abb49d7f495cb71950eefab6b27984f68d5ab9c38c36ab212b5c1d92d2dcd066ebd4a37d47952c9a17d27e1a7246bd94498ad7cb330b601ddb330fe5d0a438bbb147553cc5dac183b26606f7228428222cfd0abd631621950e398f14f3e1ee012b17593ab76cc0a65d3f96555aab737b72cc951a878d3252c449596a9734f66a649930c6f6b6a290d60706a4998742e4f18f7627545c22a7bf9835ac01833909f8de5cf815e5249ca150d08675d139cb5e3352223ba84297ff25aa5c7b89058b0439600ced989cd8961d7b417520c3262923c8405597219534b16ef366bc852074102b52a2caa21428e89040ad86f590c0d5f9bd48eac30e41aa27584a44428c5a5348abf2b4cd5525c27e66da93ec95ab25f2aa06090f539558a48c92cef52d9215a0c9fe8b27af4b26ca149beb0d1d8b91c1f454366809d4981989e5a0ab214964912ab9114b8c493420a84e9d65210afae8872d06756cc722bd4382807a591873225d28b4908e7e31a29c8024aa3cd7e73197071244588f0db9939509ca92d42b1fb37dcff37b20b71bb6f22ce1682c0cb8d5a08923dacc12eb6d1e0d99da7c22760863b1380da14745b2b93cd5523190c6003b2935e71f4757b2dd3dd163502e9d4996e8e3a520772fc5c46f365d9625e804ce00f8bb9028d627103361b3f5c51e814f88bb32ca029ce7f58da0338ac0b1a7356d3f8bd47d9531ac56005b9d20c9ec083f923e20b908247905d0d38f7d6423566efa288474957ecf6449c3b12a792fad9cfd40e896c5fc43d1d6e708506b2ce7685f9dd3c61dd131b3a191c0808402cb9cf711e05db75a8de9eaa5b97c00e3489f2a3051d1aa681a1451798d363a01a687a4c81393dfa308126a73b645053a332ad34314d0236b32c6ee6851dbca054427a85d551230d56f9b05afdc5fbe7060aac6ad98f0653e0013411eab339ad0a0e8026557d35a784dff733c32a3e99d320aff76922e49bc598ca07fccc98caafe694f0137d9a0cfe6449837ae52d10a3cacca9b20ff1699a6c893945eeb53d4d922bb2e0927e53cf30491734a2a790a8bc86790fe22b7a9aba586aa595fc7b7a4e3df9e206b4253cf365163e5f7c72cff87eb1d862b54bc8fb7b66a92bbf4e663591246bec9716319b6abfc818d0e867b991adfbea455f192e55ef679960a9fa6c490bbf901588e1ef93542065eacd46818abc793b0552d050fb17e6b129eb5eb42f64d5349917a48c0bd7bda055d3b353d39194419769aad381f2b4c37eead54dfb7119f81927b374169fcc6930afa1684a4cc177b9835126a2ea54a438ceb2d70f45bb7e94435cf66db71a4d5563022b802acb6ab300e42783edfcbf9c44bf110039c8a7a9a80ef7479babb6f3d3726eece665283bec7b2ed824ae7cc52cf3f7aa65b1aca05afca44e2ecb492b12675c9a5f948ad19ce444e4f757a8aeedb952dea031e24a7fd358c5189f6eb79e4cfe2c469182534e252e379d49f9794d4391a96fff59cda3fcfc6738e5aa9f43e1e484af52ba83f204853b2959e0538b7a706fe118a3a83273c826e074b67c4d52b8decf2aec2f7f0dce033f4fd4930a3720f49f9032174f4c67470787471c58f87480bbe749e20592531f11bd9b9db2015ec6fa994c6b41136c31b3580cedf02b88dd6710ff610d5efed8052c76762e9276048b2da36582e622c264e7a214ae095e871e7c399dfd336f76e25cffe373d572cfb98b912a9f3807ce6fedf1b589988d98285bb33cf00fa4bb79bc2e3d271942bb2b91580d4c2429e0364b2967f0202d6199bba2cba02e4b89e6904a763acf822ca77ef89aeb7c4b9c6583215bc32a2b46fcb6115101aaf7d1b71fb50c4159e9181ba324b7a2284142ee8a5e272254211d37a1a544396eba1ec8518f9bb0a6443c6e626cd5c98aed22415ae69ff79cebe453e8ffba45050f481adc5ac159561740278ae38e9d8d895aacd5ad176a4950c5a39ab6327411b9d4825ca7e8a4a5f07efb1f40fdec2cbab917413d3ba33da6dedb23797e1fbee68146d33409fe9ac2d4c9b52407aab3f3203bb4999124a1765651fad87974a7265dc2f1b55b18943940d3678bb6387763c7072cfa9d153745d3212c517d78b253b98566e17936e811c273cda18a99d0d5a96b1b5c94fa892afbb14bcee166ad327303a7066dd3708da7cec20b2a6679d7dce0604ad41b68626b981afc367d6054c441f04054ef56c49ea6823763067b3822c68c043e603c90c321312e74377c2d3ce9f4200d27a24c38e1331e80e1d0caa4baed3b3165b2862b1c139f70e8c54c737377ccd56c6a7083d20b3a13531d76df2363c4040a6b1714497b6dce724b378632c96e565113294ea074e2be4b35329fd2c17568749f548309d2fdcecacaabed5e2c63e5b2a6b2bf32c2f39c48445c24ba4783ef1c3a1e565cb09f58386c70f378520ac5a404256cf4b91a0d99ead1dc1f9fbc124d3aa0195381060b67acd567dc88b8218aee548073c75cdc6a9e734d63851b1d195704a1e2e714bfdc611f37a8916f8bd71fa733ef31bbb35c9c92a8501105e264c114a9931229f9fccf9c4971dbea9173cd8073f5dd2a1040757de334a7b66f5c47dfb7027793efbbda8d085d5645b29ed440867c17787d10e8e3ef32e20a9c3c9de00a8fa1955b51452f3639169daae39a4eeb3a34eb8c4b1b28e7a94e9ee633c62eec2aa3abeb4f6e7883c11cb3f8ab72006d69ea986e2687ed9e0290b1f5f0941e4e0141d3dda0bbc32d6e3568c6b52a6053da0fba2798622988ec68eaacc20daec522563cd4ed64c0523f297926defdf009be69fdf0e56f82c5ad1ec5bc94e991062e6650bb815eb6b66b2b2d9900b4722bc745471972b88fee065c6bd0460fefbb588d2531880814d0f9c0d5a66cf4babfa5250f35e89ee1a3a5ca2a53d44154de022e5a0437401beb6d983d162c7ebb8089bfaa482c10cd10bacc96baac731d3e456467cf7144aa7077116f600a3cb4df3e8b53ff09b8292acede09e67f613a7f7b95bd567d84de7578b74d37db140d19ae1f03e65d529621d0f59f6362b33c2feef2abfc491743406cfad9fbcabbf0fdd60fbc92ef2bc9854805892cf5801f40647399660f2156af25a5db28342484c557664c1ee07a132062c95db8045f6113de90fa7d802be0be560fb55444ea278215fbe2c207ab18ac134ca36a8f7e453aecad5f7efc2fd363d27de69f0000	6.2.0-61023	T
201801160735003_ChangeAdminAccount	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5d5b6fdcba117e2fd0ff20ec535bf8787d6982d4589f03c797d638f1a559e7a06f012dd16b215a698fa44d6c14e797f5a13fa97fa194444abc8bd47dd32240608be4cc70381c0e87e4e7fffcebdf8b9f5ed681f315c6891f85a7b3c3fd8399034337f2fc70753adba64f3fbc9bfdf4e3ef7fb7b8f4d62fce2fa4de71560fb50c93d3d9739a6e4ee6f3c47d866b90ecaf7d378e92e829dd77a3f51c78d1fce8e0e02ff3c3c3394424668896e32c3e6ec3d45fc3fc17f4eb7914ba70936e417013793048f07754b2cca93ab7600d930d70e1e9ccff398cbecd9cb3c00788f912064f330784619482148976f22981cb348ec2d572833e80e0e1750351bd271024108b7c52553795fee028937e5e3524a4dc6d92466b4b8287c7581d73be7923a5ce4a7521855d22c5a6af59af73a59dcecec2e41b8c670ecfeae43c88b36a58a3fb85e6f78bea7b4efe71af1c716418d9bf3de77c1ba4db189e86709bc620d873eeb78f81effe0c5f1fa22f303c0db741400b84444265cc07f4e93e8e36304e5f3fc2272ce6b53773e66cbb39dfb06c46b529ba701da6c74733e71631078f012cc79beaee328d62f85718c218a4d0bb07690ae330a301738d09dc395ec8445354933044368666c8ccb9012f1f60b84a9f4f67e8c79973e5bf408f7cc1427c0a7d34a150a334de428990358c6398097c81fe23ccb39f1fd0f4b1a6f569e3a969e99bfe7d0b934c8ff53ad7d339db6cd0148d2b32124d1e1ebdeb4493b7e0abbfca475f2ec4ccf90883bc3c79f6378533d9c7659f8b89902041e268fd310aaa66a4e8f3038857303389485ebe8cb6b16b2116d1b1542e5228138c2f1324132ac8445bcc2be7a17729447da63ea5a8bf6b4ea547cbe4785ef97192663f6a58bf39e883f307301263e43de248c3f5e8cd1b23b67a2ec8df7b99a1166cdebfa60d3414b978696eab213d9fcb35f003ad42de76c505ad654f7ebc86a5a5bf8f909700a1b56eee41927c8b62ef6f2079ee6065d4335b42771b2347b44cc17ad33bb7fbe72884b7dbf563653e43f0ea6c681ebe4557c04531cf6598b56a4d0f4d832fd136bd0cf3e8e153eada061025814ec439735d982457c898a1771e6dabc0ac5954922d4f356ed074fe350f4ac8bade2e28212bbb2a2821a181a958e701f0d77aa9481551a8a24429132eb615e943b4f243bd48a48a285251a2140917db8a44022cbd54542d51b0b250295b55c356bc8c8e5e345c43142b2f508a5494da8af3106d7c572f0fa9220a54942825c2c532918c035cb229cc08e636aa0b756fcabdf959b2b985e93e69bd5fd0bd8a114db44a7ed917c8ee39c68daba8f9c8346a3e3e7c7c3a7ef7e62df08edffe191ebff91eb7e583ed25655e3163da7b709073fa0504dbae59359a0db97bec7e36e464a73f1b7231d1e7af7ebeab98d7b720951179a3fac49e6de71c27d9d0d381e9e6d0cc87f101c6d3a5cae218a64748835dcb8ff4efdd1ffc34d0c7e46649024b83ba80891bfb9b9a5dffe1c14117fbfe692444357b0fabc4231f952933935da66a0dc36a55ba560cbc3b8863cb8e8b812c57a4d65917a16c4ec3d815e5b5ffef87785ea36448fbf5427513aee5ee4c35d73893ef667b6d31d79407231ac11a05ca19f9eee3e48ceaf4c36479102bad9a75a8c9ec1f6bdf47e49d48ac4906be7b6bdb0d4bdb9503beb173cb6a8f4e8ff7675cad729f62a910ad48aad8042c674912b97e2e15bb0095e126dbc5cbd0736a12df8596a9e50ae91ad9a2bf41d6872440832c284e4db55c3028aaf8460d4bf44f025164a630ce9403b213b7044d023f4c459bf643d7df8040df2bae99e164c8d45e32e04b2ee0263b1d0d537dcf4d38537b185180920f374febd4b39853b6616632f820a26e6cf953090383e17b75175ec000a6d039738b6b5fe7207181274e4964f69ea13c12539324a27bb53a5633031a1dab0213c69330387ccc5437c0fc99d3d806c71d72290c0ee77a7b35385633031a1cab82c91b9c9084510db13a23530d7295a11cc4ead4b7d3065a53553a19c0dc547d37614d5fb91cd7e8f0beba7680f98456bdc98923ac262b311b9cddd2d26ce0d6ab74439d27951cda77127e4ab2964d94d9ca370b7d1bd03d0bfddfa530b4b84b5137c4dcc58ab16302f62687222428f20cbd5a1da396012d8ee9ffe4e3012e115b37b86ac7ac30369d5f56a57a7b73cb921c854a365dc24234699939f734cd346992e1e79a5a4b034c38b5264c98cb13c6bdccba2261953dfc412d608c05c8cfc6f2d7402fa924e58a3a84b3ae09cedaf11691115dc2943f79add2635c482ccc439600ced989cdc9c4ae692fa41864c42479080bb2e432a6962cde6dd690a50e82046a55585443841c130914b0dfb2e81abe36a9ed5931916b88d6119212a10c97b228feae30554b719d989f4bf549d652fcd2008509599f53a58894c2f22e95eda245f7892dab7b2fcb169ae40b1bf59dcbf151346413b0332d90a9a7d6822c856592c46aa4052ef1a4d00211bab516c4ab2ba21ef49915b3dc0ad50fca4169f4a14c89f4322584f3718d166401a5d166bfb90eb838922244e4ed6c3a5092a9678462f76fb8ff6f342fc4edbe893a5b28022f376a2548f6b006bbd8469d67779e0a9f8005ee4c01eaa9a0db5a996cae1ae9608039203be9157b5fb7d732dd6d513d900e9de9e6a8237320c7cf65345f962de605c604feb0982bc028163760b3f1c315054e81bf38cb0299e2fc87a53d7ec3baa031672d8ddf7b949cd228062bc89566e8041ecc1f115f80143c82ec6ac0b9b716aa317b17453c4a58b1db1371ec489c4aea673f533b24b27d11f774b8c115eac83adb15e677f384754f6ce86468202000b1e43ec779146cd7a17a7baa6e5de23ad024ca8f167468980686165d604e8f816aa0e93105e6f4e8c3049a9cee90414d8dcab4d2c43409d86c6671232fece005a312d22bac8d1a59b0ca87d5da2fde3f37306055cb7e2c98020fa089509fcd6955700034a9eaab3925fcbe9fe956f1c99c0679bd4f1321df2cfa543ee067fa547e35a7849fe8d364f0274b1ad42b6f811855664e957d884fd3644bcc2972afed69925c918594f49b7a4648baa0113d8546e535cc3988afe869ea62a99555f2efe939f3e48b1bd096c8cc9759f87cf1c93de3fbc5628bd52e21efef99a5aefc3a99d54492acb15f5ac46caafd226340a39fe546b6eeab177d65b854bd9f6582a5eab3252dfc42562086bf4fd28094a9371b032af2e6ed0c484143ed5f98c7a6ac7bd1be9055d3645e90322e5cf782564dcfce4c4732065da6a9ce06cad30efba15737edc765e0679cccd2597c32a7c1bc86a2293105dfe50e469988aa3391e238cbde3e14edfa310e71d9b7dd6a34358d09ac00aa2cabcd02909f0cb6f3ff7212fd4600e4209fa6a23adc1f6dacda8e4fcbb1b11b97a1e661df63c12671e52b6699bf572d8b6505d5e2277572594e5a9138e3d2fca2568cc6242722bfbf42b1b6974a7983c6482afd4d6395607cbadd7a30f9b318450a4e3994b8dc7424e5e7350d55a6befd67358ef2f39fe18cab7e0c859313be4ae90eca1314eea464814f2deab1bd85638ca2cacc219b80d3d9f23549e17a3fabb0bffc35380ffc3c514f2adc80d07f42c65c3c319d1d1d1c1e7158e1d3c1ed9e278917484e7d44f06e76c8067819eb673aad054db0c5cc6221b4c3af20769f41fc873578f96317b0d8d9b948da112cb68c96099a8b08939dab52b826781d7af0e574f6cfbcd98973fd8fcf55cb3de72e46a67ce21c38bfb5c7d7266a3612a26ccdcac03f90eee6f1baf49c6408ebae5462d53191a480db2ca59cc183b48465ee8a2e83ba2c259a432ad9d93c0bb29cfae16b6ef32d71960dba6c0dabace8f1db464405a8de47dfbed7320465a5636c8c92dc8aa20409b92b7a9da8508574dc849612e5b8e97a20473d6e229a12f1b8c964ab4e566c1709d232ffbce75c279f42ffd72d2a7840dae0d60a6e66750174a238eed8d998a8c55add7aa19604553caa69ab892e22975a90eb149db454de6fff03a89f9d4537f722a86767b4c7b47b7b24cfefc3d73cd0689a26c15f53983ab995e44075761e648736339224d4ce1a4a1f3b8feecca44b38be760b83320768fa6cd116e76eecf88045bfb392a6683ac44c541f9eec546ea159789e757a84f05c73a862a67475eada0617a57ea04a3e76c939dcac55666ee0d4a06d1aaef1d0597841c528ef9a1b1ccc887a034d6c0d5383dfa60f8c8a38081e88eadd8ac8692a783366b0872362cc48e003c603391c12e34277c3d7c2934e0fd27022c684133ee301180e6d4caadbbe1333266bb8c231f109875ecc343777c75ccda6063728bda03331d361f73d32414ca0b076c190b4d7e62cb774631893ec66153590e2004a07eebb3423f3211ddc8646f749359820ddefacacbcdaeec532562e6b2afb2b233ccf8944c445a27b34f8cea1e361c505fb8985c306378f2765504c4a5022469fabd190a91ecdfdf1c91bd1a4039a310d68b070c6da7cc68d881ba2e84e053877ccc5ade639d73456b8d1917145102a7e4cf1cb1df671831af9b678fd713af31eb33bcbc529890a1551204e164c913a299192cfffcc9914b7ad1e39d70c3857cf568100aae38dd39c5adeb88e9eb7027793e75ded46049655918c931ac8906781d707813efe2e23aec0c9d329aef0185abd1555f46a9363d1a918d730ad6368c68c4b1b28c7a94e9fe623c62eecaa4957c74f3ef106833966f157e500dad2d431dd4c0edb3d052063ebee293d9c0282a6bb4e77875bdcaad38c6b55c0a6b4ef744f30c55210d9d1cc59851b5c8b45ac78a8db4987a57e52f24cbcfbee137cd3faeecbdf048b5b3d4a78a9d023755ccca07603bd6c3dafadac6402d0caad1c171d65c8e13ebaeb70ed84367a78dfc56a2c894144a080ce3bae9eca46affb5bcee4a13add337cb4d45865863a88c95bc0458be0066863bd0db3c782c56f1730f157158905a2194297d9529775aec3a788ecec39894815ee2ee20d4c8187f6db6771ea3f013745c5d93bc1fc2f4ce76fafb2d7aa8fd0bb0eefb6e9669ba22ec3f563c0bc4bca32043afe3926362bf3e22ebfca9f74d10524a69fbdafbc0bdf6ffdc02be5be925c885490c8520ff80144369669f61062f55a52ba8d424342587d65c6e401ae37012296dc854bf01536910d99df07b802ee6bf5504b45a47e2058b52f2e7cb08ac13ac134aaf6e85764c3defae5c7ff02471e46f1e59f0000	6.2.0-61023	T
201801160827562_SeedTopics	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5d5b6fdcba117e2fd0ff20ec535bf8787d6982d4589f03c797d638f1a559e7a06f012dd16b215a698fa44d6c14e797f5a13fa97fa194444abc8bd47dd32240608be4cc70381c0e87e4e7fffcebdf8b9f5ed681f315c6891f85a7b3c3fd8399034337f2fc70753adba64f3fbc9bfdf4e3ef7fb7b8f4d62fce2fa4de71560fb50c93d3d9739a6e4ee6f3c47d866b90ecaf7d378e92e829dd77a3f51c78d1fce8e0e02ff3c3c3394424668896e32c3e6ec3d45fc3fc17f4eb7914ba70936e417013793048f07754b2cca93ab7600d930d70e1e9ccff398cbecd9cb3c00788f912064f330784619482148976f22981cb348ec2d572833e80e0e1750351bd271024108b7c52553795fee028937e5e3524a4dc6d92466b4b8287c7581d73be7923a5ce4a7521855d22c5a6af59af73a59dcecec2e41b8c670ecfeae43c88b36a58a3fb85e6f78bea7b4efe71af1c716418d9bf3de77c1ba4db189e86709bc620d873eeb78f81effe0c5f1fa22f303c0db741400b84444265cc07f4e93e8e36304e5f3fc2272ce6b53773e66cbb39dfb06c46b529ba701da6c74733e71631078f012cc79beaee328d62f85718c218a4d0bb07690ae330a301738d09dc395ec8445354933044368666c8ccb9012f1f60b84a9f4f67e8c79973e5bf408f7cc1427c0a7d34a150a334de428990358c6398097c81fe23ccb39f1fd0f4b1a6f569e3a969e99bfe7d0b934c8ff53ad7d339db6cd0148d2b32124d1e1ebdeb4493b7e0abbfca475f2ec4ccf90883bc3c79f6378533d9c7659f8b89902041e268fd310aaa66a4e8f3038857303389485ebe8cb6b16b2116d1b1542e5228138c2f1324132ac8445bcc2be7a17729447da63ea5a8bf6b4ea547cbe4785ef97192663f6a58bf39e883f307301263e43de248c3f5e8cd1b23b67a2ec8df7b99a1166cdebfa60d3414b978696eab213d9fcb35f003ad42de76c505ad654f7ebc86a5a5bf8f909700a1b56eee41927c8b62ef6f2079ee6065d4335b42771b2347b44cc17ad33bb7fbe72884b7dbf563653e43f0ea6c681ebe4557c04531cf6598b56a4d0f4d832fd136bd0cf3e8e153eada061025814ec439735d982457c898a1771e6dabc0ac5954922d4f356ed074fe350f4ac8bade2e28212bbb2a2821a181a958e701f0d77aa9481551a8a24429132eb615e943b4f243bd48a48a285251a2140917db8a44022cbd54542d51b0b250295b55c356bc8c8e5e345c43142b2f508a5494da8af3106d7c572f0fa9220a54942825c2c532918c035cb229cc08e636aa0b756fcabdf959b2b985e93e69bd5fd0bd8a114db44a7ed917c8ee39c68daba8f9c8346a3e3e7c7c3a7ef7e62df08edffe191ebff91eb7e583ed25655e3163da7b709073fa0504dbae59359a0db97bec7e36e464a73f1b7231d1e7af7ebeab98d7b720951179a3fac49e6de71c27d9d0d381e9e6d0cc87f101c6d3a5cae218a64748835dcb8ff4efdd1ffc34d0c7e46649024b83ba80891bfb9b9a5dffe1c14117fbfe692444357b0fabc4231f952933935da66a0dc36a55ba560cbc3b8863cb8e8b812c57a4d65917a16c4ec3d815e5b5ffef87785ea36448fbf5427513aee5ee4c35d73893ef667b6d31d79407231ac11a05ca19f9eee3e48ceaf4c36479102bad9a75a8c9ec1f6bdf47e49d48ac4906be7b6bdb0d4bdb9503beb173cb6a8f4e8ff7675cad729f62a910ad48aad8042c674912b97e2e15bb0095e126dbc5cbd0736a12df8596a9e50ae91ad9a2bf41d6872440832c284e4db55c3028aaf8460d4bf44f025164a630ce9403b213b7044d023f4c459bf643d7df8040df2bae99e164c8d45e32e04b2ee0263b1d0d537dcf4d38537b185180920f374febd4b39853b6616632f820a26e6cf953090383e17b75175ec000a6d039738b6b5fe7207181274e4964f69ea13c12539324a27bb53a5633031a1dab0213c69330387ccc5437c0fc99d3d806c71d72290c0ee77a7b35385633031a1cab82c91b9c9084510db13a23530d7295a11cc4ead4b7d3065a53553a19c0dc547d37614d5fb91cd7e8f0beba7680f98456bdc98923ac262b311b9cddd2d26ce0d6ab74439d27951cda77127e4ab2964d94d9ca370b7d1bd03d0bfddfa530b4b84b5137c4dcc58ab16302f62687222428f20cbd5a1da396012d8ee9ffe4e3012e115b37b86ac7ac30369d5f56a57a7b73cb921c854a365dc24234699939f734cd346992e1e79a5a4b034c38b5264c98cb13c6bdccba2261953dfc412d608c05c8cfc6f2d7402fa924e58a3a84b3ae09cedaf11691115dc2943f79add2635c482ccc439600ced989cdc9c4ae692fa41864c42479080bb2e432a6962cde6dd690a50e82046a55585443841c130914b0dfb2e81abe36a9ed5931916b88d6119212a10c97b228feae30554b719d989f4bf549d652fcd2008509599f53a58894c2f22e95eda245f7892dab7b2fcb169ae40b1bf59dcbf151346413b0332d90a9a7d6822c856592c46aa4052ef1a4d00211bab516c4ab2ba21ef49915b3dc0ad50fca4169f4a14c89f4322584f3718d166401a5d166bfb90eb838922244e4ed6c3a5092a9678462f76fb8ff6f342fc4edbe893a5b28022f376a2548f6b006bbd8469d67779e0a9f8005ee4c01eaa9a0db5a996cae1ae9608039203be9157b5fb7d732dd6d513d900e9de9e6a8237320c7cf65345f962de605c604feb0982bc028163760b3f1c315054e81bf38cb0299e2fc87a53d7ec3baa031672d8ddf7b949cd228062bc89566e8041ecc1f115f80143c82ec6ac0b9b716aa317b17453c4a58b1db1371ec489c4aea673f533b24b27d11f774b8c115eac83adb15e677f384754f6ce86468202000b1e43ec779146cd7a17a7baa6e5de23ad024ca8f167468980686165d604e8f816aa0e93105e6f4e8c3049a9cee90414d8dcab4d2c43409d86c6671232fece005a312d22bac8d1a59b0ca87d5da2fde3f37306055cb7e2c98020fa089509fcd6955700034a9eaab3925fcbe9fe956f1c99c0679bd4f1321df2cfa543ee067fa547e35a7849fe8d364f0274b1ad42b6f811855664e957d884fd3644bcc2972afed69925c918594f49b7a4648baa0113d8546e535cc3988afe869ea62a99555f2efe939f3e48b1bd096c8cc9759f87cf1c93de3fbc5628bd52e21efef99a5aefc3a99d54492acb15f5ac46caafd226340a39fe546b6eeab177d65b854bd9f6582a5eab3252dfc42562086bf4fd28094a9371b032af2e6ed0c484143ed5f98c7a6ac7bd1be9055d3645e90322e5cf782564dcfce4c4732065da6a9ce06cad30efba15737edc765e0679cccd2597c32a7c1bc86a2293105dfe50e469988aa3391e238cbde3e14edfa310e71d9b7dd6a34358d09ac00aa2cabcd02909f0cb6f3ff7212fd4600e4209fa6a23adc1f6dacda8e4fcbb1b11b97a1e661df63c12671e52b6699bf572d8b6505d5e2277572594e5a9138e3d2fca2568cc6242722bfbf42b1b6974a7983c6482afd4d6395607cbadd7a30f9b318450a4e3994b8dc7424e5e7350d55a6befd67358ef2f39fe18cab7e0c859313be4ae90eca1314eea464814f2deab1bd85638ca2cacc219b80d3d9f23549e17a3fabb0bffc35380ffc3c514f2adc80d07f42c65c3c319d1d1d1c1e7158e1d3c1ed9e278917484e7d44f06e76c8067819eb673aad054db0c5cc6221b4c3af20769f41fc873578f96317b0d8d9b948da112cb68c96099a8b08939dab52b826781d7af0e574f6cfbcd98973fd8fcf55cb3de72e46a67ce21c38bfb5c7d7266a3612a26ccdcac03f90eee6f1baf49c6408ebae5462d53191a480db2ca59cc183b48465ee8a2e83ba2c259a432ad9d93c0bb29cfae16b6ef32d71960dba6c0dabace8f1db464405a8de47dfbed7320465a5636c8c92dc8aa20409b92b7a9da8508574dc849612e5b8e97a20473d6e229a12f1b8c964ab4e566c1709d232ffbce75c279f42ffd72d2a7840dae0d60a6e66750174a238eed8d998a8c55add7aa19604553caa69ab892e22975a90eb149db454de6fff03a89f9d4537f722a86767b4c7b47b7b24cfefc3d73cd0689a26c15f53983ab995e44075761e648736339224d4ce1a4a1f3b8feecca44b38be760b83320768fa6cd116e76eecf88045bfb392a6683ac44c541f9eec546ea159789e757a84f05c73a862a67475eada0617a57ea04a3e76c939dcac55666ee0d4a06d1aaef1d0597841c528ef9a1b1ccc887a034d6c0d5383dfa60f8c8a38081e88eadd8ac8692a783366b0872362cc48e003c603391c12e34277c3d7c2934e0fd27022c684133ee301180e6d4caadbbe1333266bb8c231f109875ecc343777c75ccda6063728bda03331d361f73d32414ca0b076c190b4d7e62cb774631893ec66153590e2004a07eebb3423f3211ddc8646f749359820ddefacacbcdaeec532562e6b2afb2b233ccf8944c445a27b34f8cea1e361c505fb8985c306378f2765504c4a5022469fabd190a91ecdfdf1c91bd1a4039a310d68b070c6da7cc68d881ba2e84e053877ccc5ade639d73456b8d1917145102a7e4cf1cb1df671831af9b678fd713af31eb33bcbc529890a1551204e164c913a299192cfffcc9914b7ad1e39d70c3857cf568100aae38dd39c5adeb88e9eb7027793e75ded46049655918c931ac8906781d707813efe2e23aec0c9d329aef0185abd1555f46a9363d1a918d730ad6368c68c4b1b28c7a94e9fe623c62eecaa4957c74f3ef106833966f157e500dad2d431dd4c0edb3d052063ebee293d9c0282a6bb4e77875bdcaad38c6b55c0a6b4ef744f30c55210d9d1cc59851b5c8b45ac78a8db4987a57e52f24cbcfbee137cd3faeecbdf048b5b3d4a78a9d023755ccca07603bd6c3dafadac6402d0caad1c171d65c8e13ebaeb70ed84367a78dfc56a2c894144a080ce3bae9eca46affb5bcee4a13add337cb4d45865863a88c95bc0458be0066863bd0db3c782c56f1730f157158905a2194297d9529775aec3a788ecec39894815ee2ee20d4c8187f6db6771ea3f013745c5d93bc1fc2f4ce76fafb2d7aa8fd0bb0eefb6e9669ba22ec3f563c0bc4bca32043afe3926362bf3e22ebfca9f74d10524a69fbdafbc0bdf6ffdc02be5be925c885490c8520ff80144369669f61062f55a52ba8d424342587d65c6e401ae37012296dc854bf01536910d99df07b802ee6bf5504b45a47e2058b52f2e7cb08ac13ac134aaf6e85764c3defae5c7ff02471e46f1e59f0000	6.2.0-61023	T
201801160922393_AddDefaultIconNumberToAppUser	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5d5b6fdcba117e2fd0ff20ec535bf8787d6982d4589f03c797d638f1a559e7a06f012dd16b215a698fa44d6c14e797f5a13fa97fa194444abc8bd47dd32240608be470389c190e87e4e7fffcebdf8b9f5ed681f315c6891f85a7b3c3fd8399034337f2fc70753adba64f3fbc9bfdf4e3ef7fb7b8f4d62fce2fa4de71560fb50c93d3d9739a6e4ee6f3c47d866b90ecaf7d378e92e829dd77a3f51c78d1fce8e0e02ff3c3c3394424668896e32c3e6ec3d45fc3fc17f4eb7914ba70936e417013793048f07754b2cca93ab7600d930d70e1e9ccff398cbecd9cb3c007a8f3250c9e660e08c328052962ede4530297691c85abe5067d00c1c3eb06a27a4f20482066f9a4aa6ecafdc151c6fdbc6a4848b9db248dd696040f8fb138e67cf346429d95e24202bb44824d5fb351e7423b9d9d85c93718cf1cbeab93f320ceaa6189ee1792df2faaef39f9c7bd72c6916264fff69cf36d906e63781ac26d1a8360cfb9df3e06befb337c7d88bec0f034dc0601cd10620995311fd0a7fb38dac0387dfd089f309bd7decc99b3ede67cc3b219d5a618c275981e1fcd9c5bd439780c6039dfd470976914c3bfc210c62085de3d48531887190d984b4ce89deb0ba9688a6a920e918e210b993937e0e5030c57e9f3e90cfd3873aefc17e8912f98894fa18f0c0a354ae32d943059d3710c33862fd07fa4f3ece707643ed6b43e6d3c352d7dd3bf6f6192c9b15ee67a3a679b0d32d1b8222391e4e1d1bb4e24790bbefaab7cf6e54ccc9c8f30c8cb93677f5338937d5cf6b9308404311247eb8f51503523459f1f40bc82994a44f2f265b48d5d0bb6888ca57c914219637c99c0995041c6da625e390fbd4b21e233f52945fd5d732a3d6a26d7e7951f2769f6a3a6eb37077df4fc018cd431f21e71a4e9f5e8cd1ba36ef5bd207fef658a5a74f3fe356d20a1c8c54b735b09e9fbb9804f0099c1b51b85b7dbf5634bae2fd7c00fb4e27ddb01cf792f68657cf2e3352cede67d847c0e08ad79be0749f22d8abdbf81e4b9837556dfd912badb18b9b5650ad69bde7bbb7f8e42c84eeb107d7536350fdfa22be0a208ea32cc5ab5a6878cea4bb44d2fc33c16f994bab6e14849a01376ce5c1726c9155266e89d47db2acc6b16e3648b5d8d5335b5bfe6210e8912da8538244e50853824d03065eb3c00fe5acf15a9223255942879c2c5b62c7d88567ea867895411592a4a942ce1625b9648b8a6e78aaa253256162a79ab6ad8b297d1d1b3866b886ce5054a968a525b761ea28defeaf9215544868a122547b858c69271b84cb69819c15c477581f34db9d33f4b36b730dd27adf70bba5731a28956c92ffb02d93dc7b87115831f99c6e0c7878f4fc7efdebc05def1db3fc3e337dfe3267fb09da9cc2b669df61e1ce43dfd02826dd75d35b286dc3d766f0d39d9e95b43ce26fafcd5cff728f3fa16a432226f549fe8b3adcd719c0d6d0ecc3087ee7c181f606c2e554ec830d9421aec5ab6a57feffee0a7813e26374b39582ad4054cdcd8dfd4e4100e0f0ebac8224c23bdaad97b58a531f9a84c99e7ec32f16b1856ab92bf62e0dd411c5b0e5c0c64b922b5ccba0865731ac6ae28affd7f3fc4f7354abeb55f2f5467702d77672a5be354be9bedb585ad298f59348c350a9433f2ddc7c919d5e987c9f220565a351b5013eb1f6bdf47f89d48ac4926be7b6ddb0d4ddb95e3c2b173cb6a8f4ecff7675cad729f62a910ad48aad8042c674912b97ece15bb0095e1263bc4cbd0736a12df8594a9e50ac91ae9a2bf41da873840932c084e4db55c3028aaf87e0e4bf44f0251a4a630ce8403b213b70419811fa6a24efba1eb6f40a01f15d7ccd01832b1971df0251770939db586a97ee4263d537b189181b21fce4eebc4b39853ba61a632f820a26e6ef953090385e14775175ec000a6d039738b4b64e7207181279a24527bcf901f89aa4912d1bd6a1d2b9901958e158149c79350387ccc5437c1fc99d3d80ac71d7229140ee77a7b55385632032a1c2b82c92b9c9084514db13a23534d7295a11c44ebd477dd065a5355321940dd546337e99abec039aed2e17d75ed04f309ad7a951367584d56a23638bba5a5d9c0ad57e9863a4f2a39b4ef24fc94642d9b08b3956f16c636a07b16c6bf4b61687197a26e8ab98b1563c704ec4d0e454850e4197ad53a462c036a1c33fec9c7035c22b66e72d58e59a16c3abfac4af5f6e69625390a156fba8485a8d23275eec9cc346992e16d4d2da5010c4e2d0993cee509e35eacae485865cf88500b186306f2b3b1fc6dd14b2a49b9a201e1ac6b82b376bc4664449730e54f5eabf41817120b76c812c0393bb13931ec9af6428a41464c9287b0204b2e636ac9e2dd660d59ea2048a05685453544c831914001fb2d8ba1e16b93da9115865c43b48e909408a5b89446f17785a95a8aebc4bc2dd527594bf64b05140cb23ea74a112999e55d2a3b448be1135d568f5e962d34c917361a3b97e3a368c80cb0332910d3534b4196c2324962359202977852488130dd5a0ae2d515510efacc8a596e851a07e5a034f250a6447a3109e17c5c2305594069b4d96f2e032e8ea408117e3b33078a33b5452876ff86fbff4676216ef74dc4d9421078b9510b41b28735d8c5361a3cbbf354f804cc706702509b826e6b65b2b96a2483016c4076d22b8ebe6eaf65badba246209d3ad3cd5147ea408e9fcb68be2c5bcc0bc40afc613157405b2c6ec066e3872b0aea027f719605cec5f90f4b7b3488754163ce6a1abff7287b4aa318ac20579a611d78307f927c0152f008b2ab01e7de5aa8c6ec5d14f128e98add9e887347e254523ffb99da2191ed8bb8a7c30daed040d6d9ae30bf9b27ac7b624327c31601018825f739cea360bb0ed5db5375eb12258226517eb4a043833e30b4e802737a0cf0034d8f2930a7471f26d0e474870c6a6a54a69526a649c06696c5cdbcb08317944a48afb03a6aa4c12a1f56abbf78ffdc4081552dfbd1600a8a8026427d36a755810bd0a4aaafe694305a0033ace293390d8205401321df2cc654c20130632abf9a53923cf8a7494a8acd69e3e7ff343dfcc99206f5825c2046959953651ff9d334d912738adc4b7e9a245764c125fd5e9f61922e68444f2151790df31ec417fa3475b1d44ae3f9b7fa9ceaf3c50d684b78e6cb2cd613f1393fb3ae88c5162b6942def633cb68f975322b95241164bf6c89995afb05cc80463f4b992ca6500714ca50ac7a9bcb0462d5674b5af8f5ad400c7f9fa40229d37a360a54e4e4db29908286dabf300f5959f7a27d7daba6c9bc4e655cb8ee75ae9a9e9d9a8ea40cba2c569d0e942729f653af6eda8fcbc04f4499a5b3f86413d7512fadd8888e2af82e7747ca24579d8a144765f6faa168d78f7288cbbeed36a6a96a4c60055065706d1680fcd4b19dff9793e83702209704682aaa8b03a3cd55dbf969393776f332941df63d176c8258be62966703aa65b1aca05afca44e2ecb772b9272dc11822815a339c989c8efc6505ddb73a5bc9d63c495fe16b38a313e956f3d99fc398f22bda79c4a5c6e3a93f2b3a0862253df2cb49a47f9d9d270ca553f87c2a90c5fa57407e5e90c770ab3c02722f528e4c211495165e6904dc0e96cf99aa470bd9f55d85ffe1a9c077e7e08402adc80d07f42ca5c3c5f9d1d1d1c1e71a8e6d341189f278917484e9444987176ca067875eb6732ad0564b0c5e362c1bec3af20769f41fc873578f9631700ded9994bda1180b78c9609528c08e89d8b52b882781d7af0e574f6cfbcd98973fd8fcf55cb3de72e46aa7ce21c38bfb5470227623662a26ccdf2c03fbeeee661bcf40c6608edae4462353091a480302da59c418fb40490ee8a2e830f2d259ac335d9e93c0b079dfae16baef32d11a10d86dc0a00ba299f0c06b442846f6db994433e3ffaf6ecc9e09e959eb631a4732b8a12d8e6aee8752242152c73135a4a48e6a60b8c1ca2b9096b4a78e62656511dd5d8ae3aa465fe79cfb94e3e85feaf5b54f080a4c12d3e9c657581caa2383fd9d920abc5e2df7ae59744693c046b2b431761562dc8750aa55a0aefb7ff0188d2cec2a57b1181b433da63eabd3dece8f7e16b1e68e84f9368b229a69e5c4b72543d3b0fb243bb2349566b6715a58fad4c776ad2257660bb85419954347d63690bca37767cc042f5597153341dc212d5a7313b95ac68169e67831e213cd79cd298095d9d0bb70171a99fa8b21fbb6c1f6ed62ad53770aed136afd778ea2cbca0629677cd0d0ea644bd213cb6c6d4c10fe90786701c04bc44f5c846ec692ae03866188d2302e248b00ec643641c1290437765d8c2934e0f7f7122ca84133ee3a12d0ead4caaebc31353266b6cc531c114875ecc345781c75ccda6868d28bdf13331d561f73d32464c70bb764191b4f7f02cb774632893ecaa163591e2044a27eebb5423f3291d5c8746f749350026ddefacacbcdaeec532562e6b2afb2b23f0d18944c445a27b34acd1a1e361c58dfd8985c306579927a5504c4a50c2469fabd190a91ecd85f4c92bd1a4039a311568b070c65a7dc68d881b42fe4e05e577ccc5ade67dd83456b8d1617c45c42c7e4ef15320f6b5841aa6b7784e723af31eb34bd0c529890ac251204e164c913a299192cfff269b1464ae1ee6d70ce557dfad02ae54d7374e736afbc675f47d2b4042f9beabdd88d0655524eb498dbac87781d707813efe2e23ae00f5d309aef0185ab91555f4629303e7a93aaee9b4ae43b3ceb8b481729eeae4693e63ecc2ae32babafee486371826330b162b47fb96a68ee966728cf129a02e5b0f4fe9e1149836dd0dba3b90e55683665cab0287a5fda07bc2549622de8ea6ce2a90e35ae064c5cbdf4e062cf5939277e7dd0f9f80b1d60f5ffec858dcea51cc4b991e69e06206b51b9c686bbbb6d29209e040b7725c749421c70fe96ec0b5066df492bf8bd558128388c8039d0f5c6dca4670012d2d79a841f78c752d555699a20ea2f216d8d6225a02da586fc3ecb160f1db054cfc554562816886d065b6d4659debf029223b7b8e235285bb8b780353e0a1fdf6599cfa4fc04d5171f64e30ff73d8f9dbabecb5ea23f4aec3bb6dbad9a668c870fd1830ef92b20c81aeff1cc09be57971975fe54fba180262d3cfde57de85efb77ee0957c5f492e442a4864a907fc00229bcb347b08b17a2d29dd46a121212cbe3263f200d79b00114beec225f80a9bf086d4ef035c01f7b57aa8a522523f11acd817173e58c5609d601a557bf42bd2616ffdf2e37f01d567a02ae0a00000	6.2.0-61023	T
201801170613376_AddFourSeedUsersQuestionsAnswers	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5d5b6fdcba117e2fd0ff20ec535bf8787d6982d4589f03c797d638f1a559e7a06f012dd16b215a698fa44d6c14e797f5a13fa97fa194444abc8bd47dd32240608be470389c190e87e4e7fffcebdf8b9f5ed681f315c6891f85a7b3c3fd8399034337f2fc70753adba64f3fbc9bfdf4e3ef7fb7b8f4d62fce2fa4de71560fb50c93d3d9739a6e4ee6f3c47d866b90ecaf7d378e92e829dd77a3f51c78d1fce8e0e02ff3c3c3394424668896e32c3e6ec3d45fc3fc17f4eb7914ba70936e417013793048f07754b2cca93ab7600d930d70e1e9ccff398cbecd9cb3c007a8f3250c9e660e08c328052962ede4530297691c85abe5067d00c1c3eb06a27a4f20482066f9a4aa6ecafdc151c6fdbc6a4848b9db248dd696040f8fb138e67cf346429d95e24202bb44824d5fb351e7423b9d9d85c93718cf1cbeab93f320ceaa6189ee1792df2faaef39f9c7bd72c6916264fff69cf36d906e63781ac26d1a8360cfb9df3e06befb337c7d88bec0f034dc0601cd10620995311fd0a7fb38dac0387dfd089f309bd7decc99b3ede67cc3b219d5a618c275981e1fcd9c5bd439780c6039dfd470976914c3bfc210c62085de3d48531887190d984b4ce89deb0ba9688a6a920e918e210b993937e0e5030c57e9f3e90cfd3873aefc17e8912f98894fa18f0c0a354ae32d943059d3710c33862fd07fa4f3ece707643ed6b43e6d3c352d7dd3bf6f6192c9b15ee67a3a679b0d32d1b8222391e4e1d1bb4e24790bbefaab7cf6e54ccc9c8f30c8cb93677f5338937d5cf6b9308404311247eb8f51503523459f1f40bc82994a44f2f265b48d5d0bb6888ca57c914219637c99c0995041c6da625e390fbd4b21e233f52945fd5d732a3d6a26d7e7951f2769f6a3a6eb37077df4fc018cd431f21e71a4e9f5e8cd1ba36ef5bd207fef658a5a74f3fe356d20a1c8c54b735b09e9fbb9804f0099c1b51b85b7dbf5634bae2fd7c00fb4e27ddb01cf792f68657cf2e3352cede67d847c0e08ad79be0749f22d8abdbf81e4b9837556dfd912badb18b9b5650ad69bde7bbb7f8e42c84eeb107d7536350fdfa22be0a208ea32cc5ab5a6878cea4bb44d2fc33c16f994bab6e14849a01376ce5c1726c9155266e89d47db2acc6b16e3648b5d8d5335b5bfe6210e8912da8538244e50853824d03065eb3c00fe5acf15a9223255942879c2c5b62c7d88567ea867895411592a4a942ce1625b9648b8a6e78aaa253256162a79ab6ad8b297d1d1b3866b886ce5054a968a525b761ea28defeaf9215544868a122547b858c69271b84cb69819c15c477581f34db9d33f4b36b730dd27adf70bba5731a28956c92ffb02d93dc7b87115831f99c6e0c7878f4fc7efdebc05def1db3fc3e337dfe3267fb09da9cc2b669df61e1ce43dfd02826dd75d35b286dc3d766f0d39d9e95b43ce26fafcd5cff728f3fa16a432226f549fe8b3adcd719c0d6d0ecc3087ee7c181f606c2e554ec830d9421aec5ab6a57feffee0a7813e26374b39582ad4054cdcd8dfd4e4100e0f0ebac8224c23bdaad97b58a531f9a84c99e7ec32f16b1856ab92bf62e0dd411c5b0e5c0c64b922b5ccba0865731ac6ae28affd7f3fc4f7354abeb55f2f5467702d77672a5be354be9bedb585ad298f59348c350a9433f2ddc7c919d5e987c9f220565a351b5013eb1f6bdf47f89d48ac4926be7b6ddb0d4ddb95e3c2b173cb6a8f4ecff7675cad729f62a910ad48aad8042c674912b97ece15bb0095e1263bc4cbd0736a12df8594a9e50ac91ae9a2bf41da873840932c084e4db55c3028aaf87e0e4bf44f0251a4a630ce8403b213b70419811fa6a24efba1eb6f40a01f15d7ccd01832b1971df0251770939db586a97ee4263d537b189181b21fce4eebc4b39853ba61a632f820a26e6ef953090385e14775175ec000a6d039738b4b64e7207181279a24527bcf901f89aa4912d1bd6a1d2b9901958e158149c79350387ccc5437c1fc99d3d80ac71d7229140ee77a7b55385632032a1c2b82c92b9c9084514db13a23534d7295a11c44ebd477dd065a5355321940dd546337e99abec039aed2e17d75ed04f309ad7a951367584d56a23638bba5a5d9c0ad57e9863a4f2a39b4ef24fc94642d9b08b3956f16c636a07b16c6bf4b61687197a26e8ab98b1563c704ec4d0e454850e4197ad53a462c036a1c33fec9c7035c22b66e72d58e59a16c3abfac4af5f6e69625390a156fba8485a8d23275eec9cc346992e16d4d2da5010c4e2d0993cee509e35eacae485865cf88500b186306f2b3b1fc6dd14b2a49b9a201e1ac6b82b376bc4664449730e54f5eabf41817120b76c812c0393bb13931ec9af6428a41464c9287b0204b2e636ac9e2dd660d59ea2048a05685453544c831914001fb2d8ba1e16b93da9115865c43b48e909408a5b89446f17785a95a8aebc4bc2dd527594bf64b05140cb23ea74a112999e55d2a3b448be1135d568f5e962d34c917361a3b97e3a368c80cb0332910d3534b4196c2324962359202977852488130dd5a0ae2d515510efacc8a596e851a07e5a034f250a6447a3109e17c5c2305594069b4d96f2e032e8ea408117e3b33078a33b5452876ff86fbff4676216ef74dc4d9421078b9510b41b28735d8c5361a3cbbf354f804cc706702509b826e6b65b2b96a2483016c4076d22b8ebe6eaf65badba246209d3ad3cd5147ea408e9fcb68be2c5bcc0bc40afc613157405b2c6ec066e3872b0aea027f719605cec5f90f4b7b3488754163ce6a1abff7287b4aa318ac20579a611d78307f927c0152f008b2ab01e7de5aa8c6ec5d14f128e98add9e887347e254523ffb99da2191ed8bb8a7c30daed040d6d9ae30bf9b27ac7b624327c31601018825f739cea360bb0ed5db5375eb12258226517eb4a043833e30b4e802737a0cf0034d8f2930a7471f26d0e474870c6a6a54a69526a649c06696c5cdbcb08317944a48afb03a6aa4c12a1f56abbf78ffdc4081552dfbd1600a8a8026427d36a755810bd0a4aaafe694305a0033ace293390d8205401321df2cc654c20130632abf9a53923cf8a7494a8acd69e3e7ff343dfcc99206f5825c2046959953651ff9d334d912738adc4b7e9a245764c125fd5e9f61922e68444f2151790df31ec417fa3475b1d44ae3f9b7fa9ceaf3c50d684b78e6cb2cd613f1393fb3ae88c5162b6942def633cb68f975322b95241164bf6c89995afb05cc80463f4b992ca6500714ca50ac7a9bcb0462d5674b5af8f5ad400c7f9fa40229d37a360a54e4e4db29908286dabf300f5959f7a27d7daba6c9bc4e655cb8ee75ae9a9e9d9a8ea40cba2c569d0e942729f653af6eda8fcbc04f4499a5b3f86413d7512fadd8888e2af82e7747ca24579d8a144765f6faa168d78f7288cbbeed36a6a96a4c60055065706d1680fcd4b19dff9793e83702209704682aaa8b03a3cd55dbf969393776f332941df63d176c8258be62966703aa65b1aca05afca44e2ecb772b9272dc11822815a339c989c8efc6505ddb73a5bc9d63c495fe16b38a313e956f3d99fc398f22bda79c4a5c6e3a93f2b3a0862253df2cb49a47f9d9d270ca553f87c2a90c5fa57407e5e90c770ab3c02722f528e4c211495165e6904dc0e96cf99aa470bd9f55d85ffe1a9c077e7e08402adc80d07f42ca5c3c5f9d1d1d1c1e71a8e6d341189f278917484e9444987176ca067875eb6732ad0564b0c5e362c1bec3af20769f41fc873578f9631700ded9994bda1180b78c9609528c08e89d8b52b882781d7af0e574f6cfbcd98973fd8fcf55cb3de72e46aa7ce21c38bfb5470227623662a26ccdf2c03fbeeee661bcf40c6608edae4462353091a480302da59c418fb40490ee8a2e830f2d259ac335d9e93c0b079dfae16baef32d11a10d86dc0a00ba299f0c06b442846f6db994433e3ffaf6ecc9e09e959eb631a4732b8a12d8e6aee8752242152c73135a4a48e6a60b8c1ca2b9096b4a78e62656511dd5d8ae3aa465fe79cfb94e3e85feaf5b54f080a4c12d3e9c657581caa2383fd9d920abc5e2df7ae59744693c046b2b431761562dc8750aa55a0aefb7ff0188d2cec2a57b1181b433da63eabd3dece8f7e16b1e68e84f9368b229a69e5c4b72543d3b0fb243bb2349566b6715a58fad4c776ad2257660bb85419954347d63690bca37767cc042f5597153341dc212d5a7313b95ac68169e67831e213cd79cd298095d9d0bb70171a99fa8b21fbb6c1f6ed62ad53770aed136afd778ea2cbca0629677cd0d0ea644bd213cb6c6d4c10fe90786701c04bc44f5c846ec692ae03866188d2302e248b00ec643641c1290437765d8c2934e0f7f7122ca84133ee3a12d0ead4caaebc31353266b6cc531c114875ecc345781c75ccda6868d28bdf13331d561f73d32464c70bb764191b4f7f02cb774632893ecaa163591e2044a27eebb5423f3291d5c8746f749350026ddefacacbcdaeec532562e6b2afb2b23f0d18944c445a27b34acd1a1e361c58dfd8985c306579927a5504c4a50c2469fabd190a91ecd85f4c92bd1a4039a311568b070c65a7dc68d881b42fe4e05e577ccc5ade67dd83456b8d1617c45c42c7e4ef15320f6b5841aa6b7784e723af31eb34bd0c529890ac251204e164c913a299192cfff269b1464ae1ee6d70ce557dfad02ae54d7374e736afbc675f47d2b4042f9beabdd88d0655524eb498dbac87781d707813efe2e23ae00f5d309aef0185ab91555f4629303e7a93aaee9b4ae43b3ceb8b481729eeae4693e63ecc2ae32babafee486371826330b162b47fb96a68ee966728cf129a02e5b0f4fe9e1149836dd0dba3b90e55683665cab0287a5fda07bc2549622de8ea6ce2a90e35ae064c5cbdf4e062cf5939277e7dd0f9f80b1d60f5ffec858dcea51cc4b991e69e06206b51b9c686bbbb6d29209e040b7725c749421c70fe96ec0b5066df492bf8bd558128388c8039d0f5c6dca4670012d2d79a841f78c752d555699a20ea2f216d8d6225a02da586fc3ecb160f1db054cfc554562816886d065b6d4659debf029223b7b8e235285bb8b780353e0a1fdf6599cfa4fc04d5171f64e30ff73d8f9dbabecb5ea23f4aec3bb6dbad9a668c870fd1830ef92b20c81aeff1cc09be57971975fe54fba180262d3cfde57de85efb77ee0957c5f492e442a4864a907fc00229bcb347b08b17a2d29dd46a121212cbe3263f200d79b00114beec225f80a9bf086d4ef035c01f7b57aa8a522523f11acd817173e58c5609d601a557bf42bd2616ffdf2e37f01d567a02ae0a00000	6.2.0-61023	T
201801171056125_AddMoreSeedData	iKnow.Migrations.Configuration	\\x1f8b0800000000000400ed5d5b6fdcba117e2fd0ff20ec535bf8787d6982d4589f03c797d638f1a559e7a06f012dd16b215a698fa44d6c14e797f5a13fa97fa194444abc8bd47dd32240608be470389c190e87e4e7fffcebdf8b9f5ed681f315c6891f85a7b3c3fd8399034337f2fc70753adba64f3fbc9bfdf4e3ef7fb7b8f4d62fce2fa4de71560fb50c93d3d9739a6e4ee6f3c47d866b90ecaf7d378e92e829dd77a3f51c78d1fce8e0e02ff3c3c3394424668896e32c3e6ec3d45fc3fc17f4eb7914ba70936e417013793048f07754b2cca93ab7600d930d70e1e9ccff398cbecd9cb3c007a8f3250c9e660e08c328052962ede4530297691c85abe5067d00c1c3eb06a27a4f20482066f9a4aa6ecafdc151c6fdbc6a4848b9db248dd696040f8fb138e67cf346429d95e24202bb44824d5fb351e7423b9d9d85c93718cf1cbeab93f320ceaa6189ee1792df2faaef39f9c7bd72c6916264fff69cf36d906e63781ac26d1a8360cfb9df3e06befb337c7d88bec0f034dc0601cd10620995311fd0a7fb38dac0387dfd089f309bd7decc99b3ede67cc3b219d5a618c275981e1fcd9c5bd439780c6039dfd470976914c3bfc210c62085de3d48531887190d984b4ce89deb0ba9688a6a920e918e210b993937e0e5030c57e9f3e90cfd3873aefc17e8912f98894fa18f0c0a354ae32d943059d3710c33862fd07fa4f3ece707643ed6b43e6d3c352d7dd3bf6f6192c9b15ee67a3a679b0d32d1b8222391e4e1d1bb4e24790bbefaab7cf6e54ccc9c8f30c8cb93677f5338937d5cf6b9308404311247eb8f51503523459f1f40bc82994a44f2f265b48d5d0bb6888ca57c914219637c99c0995041c6da625e390fbd4b21e233f52945fd5d732a3d6a26d7e7951f2769f6a3a6eb37077df4fc018cd431f21e71a4e9f5e8cd1ba36ef5bd207fef658a5a74f3fe356d20a1c8c54b735b09e9fbb9804f0099c1b51b85b7dbf5634bae2fd7c00fb4e27ddb01cf792f68657cf2e3352cede67d847c0e08ad79be0749f22d8abdbf81e4b9837556dfd912badb18b9b5650ad69bde7bbb7f8e42c84eeb107d7536350fdfa22be0a208ea32cc5ab5a6878cea4bb44d2fc33c16f994bab6e14849a01376ce5c1726c9155266e89d47db2acc6b16e3648b5d8d5335b5bfe6210e8912da8538244e50853824d03065eb3c00fe5acf15a9223255942879c2c5b62c7d88567ea867895411592a4a942ce1625b9648b8a6e78aaa253256162a79ab6ad8b297d1d1b3866b886ce5054a968a525b761ea28defeaf9215544868a122547b858c69271b84cb69819c15c477581f34db9d33f4b36b730dd27adf70bba5731a28956c92ffb02d93dc7b87115831f99c6e0c7878f4fc7efdebc05def1db3fc3e337dfe3267fb09da9cc2b669df61e1ce43dfd02826dd75d35b286dc3d766f0d39d9e95b43ce26fafcd5cff728f3fa16a432226f549fe8b3adcd719c0d6d0ecc3087ee7c181f606c2e554ec830d9421aec5ab6a57feffee0a7813e26374b39582ad4054cdcd8dfd4e4100e0f0ebac8224c23bdaad97b58a531f9a84c99e7ec32f16b1856ab92bf62e0dd411c5b0e5c0c64b922b5ccba0865731ac6ae28affd7f3fc4f7354abeb55f2f5467702d77672a5be354be9bedb585ad298f59348c350a9433f2ddc7c919d5e987c9f220565a351b5013eb1f6bdf47f89d48ac4926be7b6ddb0d4ddb95e3c2b173cb6a8f4ecff7675cad729f62a910ad48aad8042c674912b97ece15bb0095e1263bc4cbd0736a12df8594a9e50ac91ae9a2bf41da873840932c084e4db55c3028aaf87e0e4bf44f0251a4a630ce8403b213b70419811fa6a24efba1eb6f40a01f15d7ccd01832b1971df0251770939db586a97ee4263d537b189181b21fce4eebc4b39853ba61a632f820a26e6ef953090385e14775175ec000a6d039738b4b64e7207181279a24527bcf901f89aa4912d1bd6a1d2b9901958e158149c79350387ccc5437c1fc99d3d80ac71d7229140ee77a7b55385632032a1c2b82c92b9c9084514db13a23534d7295a11c44ebd477dd065a5355321940dd546337e99abec039aed2e17d75ed04f309ad7a951367584d56a23638bba5a5d9c0ad57e9863a4f2a39b4ef24fc94642d9b08b3956f16c636a07b16c6bf4b61687197a26e8ab98b1563c704ec4d0e454850e4197ad53a462c036a1c33fec9c7035c22b66e72d58e59a16c3abfac4af5f6e69625390a156fba8485a8d23275eec9cc346992e16d4d2da5010c4e2d0993cee509e35eacae485865cf88500b186306f2b3b1fc6dd14b2a49b9a201e1ac6b82b376bc4664449730e54f5eabf41817120b76c812c0393bb13931ec9af6428a41464c9287b0204b2e636ac9e2dd660d59ea2048a05685453544c831914001fb2d8ba1e16b93da9115865c43b48e909408a5b89446f17785a95a8aebc4bc2dd527594bf64b05140cb23ea74a112999e55d2a3b448be1135d568f5e962d34c917361a3b97e3a368c80cb0332910d3534b4196c2324962359202977852488130dd5a0ae2d515510efacc8a596e851a07e5a034f250a6447a3109e17c5c2305594069b4d96f2e032e8ea408117e3b33078a33b5452876ff86fbff4676216ef74dc4d9421078b9510b41b28735d8c5361a3cbbf354f804cc706702509b826e6b65b2b96a2483016c4076d22b8ebe6eaf65badba246209d3ad3cd5147ea408e9fcb68be2c5bcc0bc40afc613157405b2c6ec066e3872b0aea027f719605cec5f90f4b7b3488754163ce6a1abff7287b4aa318ac20579a611d78307f927c0152f008b2ab01e7de5aa8c6ec5d14f128e98add9e887347e254523ffb99da2191ed8bb8a7c30daed040d6d9ae30bf9b27ac7b624327c31601018825f739cea360bb0ed5db5375eb12258226517eb4a043833e30b4e802737a0cf0034d8f2930a7471f26d0e474870c6a6a54a69526a649c06696c5cdbcb08317944a48afb03a6aa4c12a1f56abbf78ffdc4081552dfbd1600a8a8026427d36a755810bd0a4aaafe694305a0033ace293390d8205401321df2cc654c20130632abf9a53923cf8a7494a8acd69e3e7ff343dfcc99206f5825c2046959953651ff9d334d912738adc4b7e9a245764c125fd5e9f61922e68444f2151790df31ec417fa3475b1d44ae3f9b7fa9ceaf3c50d684b78e6cb2cd613f1393fb3ae88c5162b6942def633cb68f975322b95241164bf6c89995afb05cc80463f4b992ca6500714ca50ac7a9bcb0462d5674b5af8f5ad400c7f9fa40229d37a360a54e4e4db29908286dabf300f5959f7a27d7daba6c9bc4e655cb8ee75ae9a9e9d9a8ea40cba2c569d0e942729f653af6eda8fcbc04f4499a5b3f86413d7512fadd8888e2af82e7747ca24579d8a144765f6faa168d78f7288cbbeed36a6a96a4c60055065706d1680fcd4b19dff9793e83702209704682aaa8b03a3cd55dbf969393776f332941df63d176c8258be62966703aa65b1aca05afca44e2ecb772b9272dc11822815a339c989c8efc6505ddb73a5bc9d63c495fe16b38a313e956f3d99fc398f22bda79c4a5c6e3a93f2b3a0862253df2cb49a47f9d9d270ca553f87c2a90c5fa57407e5e90c770ab3c02722f528e4c211495165e6904dc0e96cf99aa470bd9f55d85ffe1a9c077e7e08402adc80d07f42ca5c3c5f9d1d1d1c1e71a8e6d341189f278917484e9444987176ca067875eb6732ad0564b0c5e362c1bec3af20769f41fc873578f9631700ded9994bda1180b78c9609528c08e89d8b52b882781d7af0e574f6cfbcd98973fd8fcf55cb3de72e46aa7ce21c38bfb5470227623662a26ccdf2c03fbeeee661bcf40c6608edae4462353091a480302da59c418fb40490ee8a2e830f2d259ac335d9e93c0b079dfae16baef32d11a10d86dc0a00ba299f0c06b442846f6db994433e3ffaf6ecc9e09e959eb631a4732b8a12d8e6aee8752242152c73135a4a48e6a60b8c1ca2b9096b4a78e62656511dd5d8ae3aa465fe79cfb94e3e85feaf5b54f080a4c12d3e9c657581caa2383fd9d920abc5e2df7ae59744693c046b2b431761562dc8750aa55a0aefb7ff0188d2cec2a57b1181b433da63eabd3dece8f7e16b1e68e84f9368b229a69e5c4b72543d3b0fb243bb2349566b6715a58fad4c776ad2257660bb85419954347d63690bca37767cc042f5597153341dc212d5a7313b95ac68169e67831e213cd79cd298095d9d0bb70171a99fa8b21fbb6c1f6ed62ad53770aed136afd778ea2cbca0629677cd0d0ea644bd213cb6c6d4c10fe90786701c04bc44f5c846ec692ae03866188d2302e248b00ec643641c1290437765d8c2934e0f7f7122ca84133ee3a12d0ead4caaebc31353266b6cc531c114875ecc345781c75ccda6868d28bdf13331d561f73d32464c70bb764191b4f7f02cb774632893ecaa163591e2044a27eebb5423f3291d5c8746f749350026ddefacacbcdaeec532562e6b2afb2b23f0d18944c445a27b34acd1a1e361c58dfd8985c306579927a5504c4a50c2469fabd190a91ecd85f4c92bd1a4039a311568b070c65a7dc68d881b42fe4e05e577ccc5ade67dd83456b8d1617c45c42c7e4ef15320f6b5841aa6b7784e723af31eb34bd0c529890ac251204e164c913a299192cfff269b1464ae1ee6d70ce557dfad02ae54d7374e736afbc675f47d2b4042f9beabdd88d0655524eb498dbac87781d707813efe2e23ae00f5d309aef0185ab91555f4629303e7a93aaee9b4ae43b3ceb8b481729eeae4693e63ecc2ae32babafee486371826330b162b47fb96a68ee966728cf129a02e5b0f4fe9e1149836dd0dba3b90e55683665cab0287a5fda07bc2549622de8ea6ce2a90e35ae064c5cbdf4e062cf5939277e7dd0f9f80b1d60f5ffec858dcea51cc4b991e69e06206b51b9c686bbbb6d29209e040b7725c749421c70fe96ec0b5066df492bf8bd558128388c8039d0f5c6dca4670012d2d79a841f78c752d555699a20ea2f216d8d6225a02da586fc3ecb160f1db054cfc554562816886d065b6d4659debf029223b7b8e235285bb8b780353e0a1fdf6599cfa4fc04d5171f64e30ff73d8f9dbabecb5ea23f4aec3bb6dbad9a668c870fd1830ef92b20c81aeff1cc09be57971975fe54fba180262d3cfde57de85efb77ee0957c5f492e442a4864a907fc00229bcb347b08b17a2d29dd46a121212cbe3263f200d79b00114beec225f80a9bf086d4ef035c01f7b57aa8a522523f11acd817173e58c5609d601a557bf42bd2616ffdf2e37f01d567a02ae0a00000	6.2.0-61023	T
201803162129428_AddFollowingsTable	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1d5d6fdcb8f1bd40ff83b04fedc1e7f54713a4c6fa0e8e1d5f8d8b1337eb1cfa66d05ada164e2bed49dac44671bfac0ffd49fd0ba5244ae2d750a4be37380408bc22391cce0c87c31972f8bffffc77f1e3f3da77bee028f6c2e07476b87f307370e0862b2f783c9d6d9387efdfcc7efce1cf7f5abc5bad9f9d5f8a7ac7693dd232884f674f49b23999cf63f709af51bcbff6dc288cc38764df0dd773b40ae74707077f9f1f1ece31013123b01c67f1691b24de1a673fc8cff33070f126d922ff3a5c613fa6df49c93283ea7c406b1c6f908b4f67decf41f875ff26c5244e08aa78e69cf91e22882cb1ff3073501084094a089a279f63bc4ca230785c6ec807e4dfbe6c30a9f780fc1853f44faaeaa62339384a4732af1a16a0dc6d9c846b4b8087c7943473b1792302cf4ad211e2bd23444e5ed25167043c9d9d05f1571ccd1cb1ab93733f4aab15d43d0f23bc9fb3623f6fb3e764257ba508104949ffed39e75b3fd946f834c0db2442fe9e73b3bdf73df767fc721bfe8a83d360ebfb2c56042f52c67d209f6ea27083a3e4e5137ea0b85ead66ce9c6f37171b96cd9836f938ae82e4f868e67c209da37b1f974c67c6bc4cc8287fc2018e5082573728497014a430704636a977a12f22b344fc92a243226864cacc9c6bf4fc1e078fc9d3e98cfc39732ebd67bc2abe50243e071e9961a451126db102c99a8e239c227c41fe2b3a4fffbe25f3c91ad6e7cd0a86a56ffacf2d8e533ad6d35c0fe76cb321f334aac028287978f4a6134a7e405fbcc78cfb6a2466ce27ec67e5f193b7c935ca3e2dbbcb27424c1089c2f5a7d0af9a154577b7287ac4a94884eaf265b88d5c0bb40a1a2bf12a0a5588896512665205156a8b79a541f47aa5209f9562c91bed9a66e9513c853e2fbd284ed23f355dbf3ae8a3e7f768a48e890a89424daf47af5e1975abef8528fd552aad79376f5f9206140a5dba48b7a590be9f0bfc80c834b872c3e0c3767ddf12eb776be4f95af2beee00e7ac17b23c3e78d11a97f3e66d48140f0aac71be4171fc358c56ff40f153078badbeb32576b711d16dcb04ad37bdf776f314069867eb107d75c69adbafe125728919f52e485bb5864726d5afe13679176406c9e7c4b5b5494a009da073e6ba388e2f8930e3d579b8ad6cbd66864ebad8d52855d3f9d7dcce294c8576764e612c40764e616d98a275ee236fadc7aaa8222395978038d1625b942e43df0fbf1206a9d12a8bef72c3a7428b2f910c2fa1d8d6227c1f3e7a819e5245159952790948295a6c4ba9c294d463c5d492112b0b41dcaa1ab6e8a570f4a8d11a325a590188525e6a8bce6db8f15c3d3e451519a1bc04c48816ab503236e58bed6f0a309b3a3aa3feba74459cc59b0f38d92f5aefe7702f2302932cdebfee4b60f71ce3c6d5d6e0c8746b707c78ff70fce6d56bb43a7efd377cfcea5b74400cb66b5629ebb4d3de6d96aca75f90bfedba2be3d9506a6bbbad6dd96cf8cd6d2115f3faaa99ba6822e563495e89b09519a657c54a4d9c95dcb12640a58bc5326985972ad8aef1a027cad8e8101708c02669342528d56ca643d664d7fc3cfd2bf0513c2c173876236f53e3bb383c3868e4bda873b0b6347c20ffaa60177564e35b690171ca816aa21bb3baf4dfca34138a60d7af866ac6daa0f250db2884a2d51f3a415ae2bcc4d77b08cc1ca093d20a938cf8683c2156911571e283a1972e635186bb69485fcafbed0eb6af162a09a459d73bd8cca5d1fd0e36033bfd1d6c8626f9fcc5cbc21d065b82a232016f545fbddba857a90266436f24b8610eddf930baafd174496763f7b325853afdc962b1714e07b44bfbe602df89495cf7d2b61b92b62be70fc60e56c11b4696df77b45a656dc8a592c1a1a86263739cc571e87a1956bcd1565a8cfc10df052ba72692965399b1f108ad892c7a1b227d0403c264897030d4d2ee63a0d2a37f3cd0ef24a0444c71941207a521fc984c022f486499f602d7db205f3f2aa199e16448c95e7620965ce04d7a782348f42337e999d986c80894fd08f3b48e3c8b39231b662243239b75bc15c39c0602238eea6370817d9c60e7cccdcfa79ea3d8452b794a12b15f19e2a310354508a957a9e32933a0d0f12430e978548193fc59108b61e756c564ea061e44e460977b850f13a9e945d8209a0c206ed0f04dba06623c03499ce44881380c7b552a0e57aec641840e3e423bd0a20ad164008983c66ed2357b2e7c5ca1a37ea85a068b4ea97a9193390c8355888d52777e279a9ec60316c2821062508cb0910e05c1aa66c900f649a319a6467d80f9a566c5e40d08e184599d85281e371bdb6215ceb701162b7519f76ab1f29419d062e549b0330257853bea78ac3849d8c9ce5a115369b24cb4921a696c030a8e34fe5dda61e7073ceb582c9cf61c5b5bf1c74b016595bb507b953a8e2c034a1c37fe9dd1543536277490b756d87416187448a6378353e17e8570d3f96265915689734fd34ce3011e7eaec1541a60c2c19430e95c1d0beb65d6e5bef8f4de356981238a40768228bb8cfd9c28a249644034a014d38084281129d0254ec4732195e75fd8ec4bf3900740c31172f36262d7b497bca72a600a176b0d58f6189b048ed9f8d58029ce764820a88aa969ce9c5b912054569405858acb2e5a0a5193de022cbd12a2859aeb831aa07580944018f9670453bc9ec5d4026e708953b23e0c55a25fcab134afeba34e0c9012595133f343b4187e3125e0d1abe2294abc85884aa3b10b511006866a1eb7a6827c2455a683decd6fe6e867c651cc780d3140ff3c0386c5b93519e4037a3219f4be6733ef33833fa3b93494009dc6bdcc09e9c49d860a2ac3d4c81dda9c06823d5a2b530d2820de889009a0738e9ab8478d44d8c4215aa75e5ae8c462118475a2ca6367e2b36ba413053f1ba0130ba43ba302239d3021004f92a12fa9113964d791c9946a41086a73c04450f8430c3c228d06cf7b310051a008774600581deab6e9261bf5463418400faa0e44c9a3afdbb79beedc9911285967bad1ee481c8a535ae5ceb02c5bccf3fc71f4c3620e249a5b5ca3cd2655ec554bfac559e659e7cebf5fdae7635be730e6bca489fbd8b2a7248cd023164ad344632b9ca502ba4009ba47e909baf3d55aaac6ed83814d49d115bfd59579576c568afae9dfcc6e9bcdbe576c8b655f016d7c4906b54ebd0dd9cd18c90e921b3a69d63fe4a3487104f23cf4b7eb00767bc0adcb746d2c88f2a3051c36fb1a078b2d3087c7656063e17105e6f0d8f03b0b4e179687a1311e7c1698c6b19fce3281f39267481230c96dc7cbab913443facc4a96a98fa68130432dfb91662635180b84f96c0eab4af6c582aabe9a43a2d9bbb861e59fcc6114b9b95820c5378b3195e9b9b831955fcd21291270b12015c5e6b0693a2e161efd640983c9e8240163cacca1f249b758987c89394421b3160b5228b2c092cd9fc521c91634820750545dc3bc073963160b5d2eb5927831779620fa627103d80a9cc5328bb5454eafc5ad3172b1c5aa1a17b9b6b825b5fc3a99554be1256cb784c99101fbc5cc00463fcb9acad6800d0dd044ab12d470065af5d912164d412301a3df27234c5a9faf8d1055f1207be1d1b4ed97ede551674ea542e79f476312e87ab061501e6db3670ed0ae9fd92c2b605b83924b4bc09b5d4cc16458ab732eda70b78c84da33186eda0f8f6ff37c15dc94cb3ff5cfe56f60d3aa708877b7fce761ef76cb3f0003b614b97bfdbca1a84d4600c3e42eeb73c6b82e59010ccf6eb599806040eefaa672919d396827166a10fdaeefc54923160a74fa6834be75c9ab967cb2e3d1504b7ddf7ce1a304eaf5b90c10992cc2656568a955da54690004f0cc0a3125994246fcd15cec63bab6c70a3cfa678495fef217849818dbb166ac18f833f0f1826ca5e5a65c55070a1b920f3ec26cc55375e0713841abe7a714b213ab946aa20cdd0921ba050d97d53f1825c5cff22a33a7b01f4e67cb17221cebfdb4c2fef237ffdcf7b2a85051e11a05de0311ec3c05c8ece8e0f04878746a3a0f40cde378c565c6035f81e2593640e6122fa5696d7e3cdb6cc4fc334cc11714b94f28facb1a3dffb58ba795d2205cd2d1d34a2a582609f3e4a79632524a679daf82157e3e9dfd3b6b76e25cfdebae6ab9e77c8c88289f3807ceefeddf682ac86c8444d99ac7414c60d34d722165206e08e9ae4862353019a4f4ec8f12729a1db6e5ab3e5dc1e51eed5102cdb256dac93cff464fe2052f99ccb77ca6c760c8ad5ee5698a27f7300f40c2d7b658aadfe1b9f7ecd153bdc1036adac6efecb482a8784ba72b789d90107a2ba7092cf09d9ca60b8cfadd9c26a8816fe634991555bcce76d5295a669ff79cabf873e0fdb62505b7841ac2e223ccacae72292a02673b6b64b558fc5baffc0a2b4d7c80a2d544971f99b000d7c9431225d17edf8d071fc6e6bff00c84a92d4c9b5918c2966f227c1b73bd0f0b51915e5c6d2e6709c63b9970ea40d8ce72e596cd0b6f62630fcc979dde339a640c6fa7a4772c1377677bd91b39d17667b0c75c94baccae3dd8f2df4ddaeab1577f3e99b5153679d321d92ec7e076ca15d56cf3950e7a84cd97261e67467438d221d785035df58c1acc7e1ddd936cebb56dccba3f3641836f825ae7406f9d9a8de663192a1f2b7c39c8447f3b5639b0a03b75724f53c9b16696c57cc4bc6a8a9439fa843abb2e442637012c34e9f432948f968f9c71e6a99328f597817cd0ecb4df6286f131538a0fbd6269ce918fb9644d2d43b8f2d0d6c44487dfdca81031c9f1b80b82a43d5639650da4396d3799956b5c311a6c05b397a1d175926112fff1ede8718c9f01ed663bf367129b2fb384fc13d97cd1088b3e57e7ae0b91c93dac890a9371b2fdb60e1c2bbb6af744c0ca689a8a1bc72855fe4414491e4fd366e7dd751932b8b637512d62b6b51a5fa0b8c883028d3eede1213dca9a5b4d9317a2496fa9c614a0c13654d6e233ee9ebce1031553799362ccc5ade6f2f13456b8d11f9d9073728a3ca5f7490b8546ddbc552855f0ffe677124f67abfbf4264d1e8c8512854bc08b0553865e9428c1678fa32bd3d8d63f4a61f62685be5b202bbed837e35a90fa64ca547d6992ce8bbd50352df540bfaba003d97b45c8d58e46025e15a9e0c3b9a175eca11b682d7b681d3d7b8004ddbabe73bda4ed3aafa2ef599d0018eab8a6d3ba0ecd3a13dca32027ebc4c59ca7bcf9004dedbafed4d37bb00746f8870fd42fe038aa1019db4cfdeece149e10b11e1ea84781bc7bed07ddc78b21720e73eda025052ee4896b3fc89ede035166ea1f4d7ca1073a6a1ffd009252743260a55e54a447e97ef84512f9fae1abf35fb414eae106dee11b274dd556ff3358886474f094492bddcc19514052b4ee06ddf5cb25d643b79aff137899a4156f597b519dd8acbb01d7aa6aa3f4415dd8550a6b524e77d4f9c061256d94a3a8a58e1e6ad03dbfbea2145695a00e22f216afadc8299a16f34fdb20cd5090ffbac0b1f75881581098017639174c59e72a78080b4f908051514538227f8d13b442093a8b12ef01b909294e9313640b5676e13b4d91718f5757c1c76db2d92664c8787def73f7ed528f92aeffec49191ee7c5c7ec4265dcc510089a5e9ad4e163f076ebf9ab12ef4bc5397d0044eaaaa21751535e26e985d4c79712d28730300444c9577ad86ef17ae31360f1c76089bee026b811f17b8f1f91fb525d408480d4338227fbe2c2438f115ac71446d59efc2432bc5a3ffff07ffbc06edd00b70000	6.2.0-61023	T
201803162301056_RenameFollowingTableToTopicFollowing	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1dd96e1cb9f13d40fea1314f49a0d5e8880dc718ed4296ac4458eb88475ee4cda07a28a9e19eeed9ee1e5b42b05f96877c527e21649fbc8a4df63dc6c280a16992c56255b158ac228bfffbcf7f173f3daf7de72b8e622f0c4e6687fb073307076eb8f282c793d93679f8e1cdeca71ffff887c5fbd5fad9f9a5a8774ceb9196417c327b4a92cddbf93c769ff01ac5fb6bcf8dc2387c48f6dd703d47ab707e7470f0b7f9e1e11c13103302cb71161fb741e2ad71fa83fc3c0b03176f922df2afc215f6e3fc3b2959a6509d6bb4c6f106b9f864e6fd1c84dff66f2926714250c533e7d4f710416489fd87998382204c5042d07cfb29c6cb240a83c7e5867c40feddcb06937a0fc88f718efedbaabae9480e8ee848e655c30294bb8d93706d09f0f03827cd5c6cde88c0b392748478ef099193173aea948027b3d320fe86a3992376f5f6cc8f68b582ba676184f73356ec676df69cb464af14012229f4df9e73b6f5936d844f02bc4d22e4ef39b7db7bdf737fc62f77e1171c9c045bdf67b122789132ee03f9741b851b1c252f1ff1438eebe56ae6ccf97673b161d98c69938de332488e8f66ce35e91cddfbb8643a33e6654246f9771ce0082578758b92044701858153b249bd0b7d119925e297141d1241235366e65ca1e70f38784c9e4e66e4cf9973e13de355f12547e253e09119461a25d1162b90ace938c214e173f25fd139fdfb8ecc276b589f362b1896bee93fb738a674aca7b91ecee96643e66954815150f2f0e84d2794bc465fbdc794fb6a2466ce47eca7e5f193b7c934ca7e5ef6399b083141240ad71f43bf6a56147dbe43d123a62211aacb97e136722dd02a68acc4ab28542126964998491554a82de69506d1eb95827c568a256bb46b9aa547f114fabcf0a238a17f6aba7e75d047cf1fd0481d131512859a5e8f5ebd32ea56df0b51fa2b2aad5937ef5e9206140add7c916e4b217d3fe7f801916970e986c1f5767ddf12ebf76be4f95af2beee00e7b417b23c3e78d11a97f3e65d48140f0aac71be4571fc2d8c56ff40f153078badbeb32576b711d16dcb04ad37bdf776fb14069867eb107d75c69abb6fe105728919f53ea0ad5ac32393ea4bb84dde07a941f229716d6d92124027e89cba2e8ee30b22cc7875166e2b5baf99a14317bb1aa56a3aff9adb3985a9d0cece298c05c8ce29ac0d53b4ce7ce4adf558155564a4b21210a7bcd816a58bd0f7c36f84416ab4eec28de796753e67d64f859ba25832c154756c0dc40fe1a317e8095754910997958084cb8b6d095758967aac985a32626521885b55c3163d0a478f5a5e43462b2d0051ca4a6dd14985408f4f514546282b0131ca8b5528195bf6c56e98024c6792cec6bf2a3d13a7f1e61a27fb45ebfd0cee45446092b5fccbbe0476cf316e5ced148e4c770ac787f70fc76f5ebd46abe3d77fc5c7afbe477fc4609b6895eea69df66ec2a43dfd82fc6dd75d19cf065e65db6d77f9b6c3ef7a0bf998d7574d516d22ef63c96089b0957da657caf0b2ff99b50d84359f29532ff86c05dbd51e745135b046c4454367b1349f2b0da6c8aef983fad7eca37862ce71ec46dea6c6c7717870d0c8cb51e7886d6911417e58c160ea722f60aa1494f34ea535bab1b74b3faf4c33a10876116ba866ac0d2a4fb68d42285afdae13a415cf4b7cbd27c1cc513a29ad30c9c890c66362158111273e18a2e9326665b8cd86f4a5bc11ef605f6ba192409a75bdb54d7d1ddd6f6d53b0d3dfdaa66892cf5fbd342c62b043282a13f046f5d59b8f7a952a6036f4be821be6d09d0fa3fb1a4d173a1bbb9f2d14eaf4278bc53e9a0e6897b6d105be1393b8eea56d37246d57ce298c1dd482378c2cbf3fe7d52a6b432e950c0e45151b9be3348e43d74bb1e28db6d262e487f83e58393511b78cca8c8d47684d64d1db10e9231810264b8483a196761f03353f22c803fd8b049488298e2871100df5c76412784122cbb417b8de06f9fa5109cd0c2703257bd98158728e37f4904790e8476ed233b30d911128fb11e6691d7916734636cc44268f80d6f1560c871a088c38aa9be01cfb38c1cea99b9d633d43b18b56f2942462bf32c447216a8ad852af52c7536640a1e34960d2f1a80227f9b32016c3cead8ac9b91b781091833df0023e4c08a7178983083380cc413430e91a88fb0c24769237056233ec5aa9d85cf91b07913cf8bced402b2b449301240e1abb49d7ec21f271852e7746d53258f44cd58b9ccc61182ca4b06a60da2a772154a8d5a750dcb0b94ad577a09a3903182ecdf5bc1affa154bd9a3d93b73184d3697546a478546d6ca356381b0718b5b957b957a396a7cc80462d4f829d11b82a2252c763c529c44e36df8ab04b9345a495d448631b5070a4f1efd2263c3b1c5ac762e1a4e8d8da8a3f9a0a28abcccbdaabd471641950e2b8f1ef8ca6aab148a143c0b5c2a633c8a07334bd99a30a0f2d849bce5d2b8bb44a9c7b9a661a27f1f0730da6d200130ea68449e7ea70592fb32e73d7d32bdca4058e7204d24346e9bdeee74411702203ca634e711eb3102582025de2443c3a520507045780340f790079c4426e5e4cec9af6928355054ce185ad01cbef3b5430c51da10940104e6d73e6908b04a1b2a72c68555c99d1d22a37ee2dc0e6174bb45033cd5003b40e9012083313181115ef7c31b5806b61e2e4ac8f5995e897122dcdf0fa101503a44456d4d1fc102d865f4c0e78f4aae08b126f21fcd268ec42c88481a19ad1ada9209f5f95e9a08f09984505987114335e430cd0992f8261116f4d0bf9489f4c0bbda3dacc55cd0c82515f1a72801ee65e268674464f4305959d6ae43b6d4e03c13cad15aca69342bc5201cc0b9d3bd5d8a16a2ed6c62ed43ae5d34263164b24ac31559e3d13df5e238d29f8e3008d5920dd191518b1850901789c0c7d4e8dc821bb984ce65a0b42e416094c0485dfc4c073d268f0bcb70310851ce1ce0800eb49dd76de6443df8806032848d5d92a79f475fb7bd31d3e330225eb4c37e41d894371e0abdc4196658b7996b22effb09803b9ed165768b3a1dabd6a997f719659a2bbb31f96f629e0d6198c392f69e27eb7ec290923f48885529adb6c85d3ec43e72841f7881ec63b5bada56adc7e19d8b2145df15b629977c556a6a84fff6676e56cc2bf62fb2cfb14f2c61764506bea95482fd9480692dcd0a18906918f22c569cab3d0dfae03d83d02b72e33c4b120ca8f1670d8846f1c2cb6c01c1e97f48d85c71598c36383f82c385d701f86c678fa59609a00009d6502e7250f922460927b8f9757236986f499952ce7be9c06c20cb5ec479a996c642c10e6b339ac2abf180baafa6a0e294f18c60d2bfb640ea34807c60229be598ca9cc08c68da9fc6a0e4991f38b05a92836879d670063e1e59f2c613049a424604c9939543ecf170b932f31872824f362410a451658b229bb3824d98246f0008aaa6b98f72027e962a1cba556122fa6eb12445f2c6e005b81b35866b1b6c819bdb835462eb65855e322bd17b7a4965f27b36a297c88ed9630398260bf9819c0e8675953d91ab0a1019a6855121cce40ab3e5bc2cad3dc48c0f2ef931126d155d54e9284b891bd18d501e8570acaf3d39c86850e558fcbb34e58d59443434d6e591fdbda975cc203de0a630a26c35a9dafd186bb65d8d49ec170d37e787c9765c2e0a65cf6a97f2e7f077b58857fbc3b6b208b91b7b3060018b0e1c8650ce0ed466d9a031826970680b3cd756910607876abcd040403f2de37958bf480423bb15083e8777d2f0e28b150a0434ba3f1ad4b5eb5e4931d8f865aeafbe60b1f3450afcf65bcc864112e2b434badd2a6a2f110c0512b8498640a19f147735b90e9da1e2bf0c4a01156fa1b65106262a8c79ab1621cd0c0e50bb2352f37e5aa3a6ed8907cf0c9672b9eaae390c3095a3d3fa5089e58a5541365244f88d82df2e859fd935552382dab32730afbe164b67c21c2b1dea715f697bffa67be9706898a0a5728f01e886067c9456647078747c2b357d379826a1ec72b2ee71ef80e15cfb20172a27894a6b599f76c1320f30f41055f51e43ea1e84f6bf4fce72e1e77a231b9a4a3c79d54b04c52f1c98f3da5a4948e485f062bfc7c32fb77daecad73f9afcf55cb3de72622a2fcd639707e6bff4a5441662324cad63c0e626a9c6ed21629e3724348774512ab81c920a58787949069ded996ef0a7505977b36480934cd876927f3fc2b418917bca432dff2a1208321b77a17a8299edcd34000095fdb62a97e09e8deb3474ff50a10a8691bbff4d30aa2e2359faee0754242e8b59e26b0c0977a9a2e30ea977b9aa006bedad3645654e13bdb55a768997ede732ee34f81f7eb9614dc116a088b8f30b3bacad2a888a3edac91d562f16fbdf22bac34f1cd8b56135d7ed7c2025c776f579494fb6d379e97185b088447274c0de2bc9985356cf9e4c2f731e1fb301315d9cbd536739abfbc9359a78e86ed2c57eed8b4f32686f6c07cd9e98da34942f2764a7ac7127d77b6a1bd95f37877067bcc45a9cbe4dd832dffdd64c51e7bf5e773655b6193351d92ed72206ea7fc51cd766074d023ecc034413933a2c3e10eb92e1cedaa67d460f6ebe8ee645bd76d63d6fdbe091a7c13d43ac57aebb46e792e97a132bdc217864cf4b763953f0bba6727f73495fc6c6649d247ccc9a648b7a34fc6b3eb4264723bc042934e2f01fab8e9ce198f9e2601537f59ce874f74fb3da6321f3377f9d00b98e66cf9982bd8d452912b0f724d4c74f8bd8e0a11937491bb2048daa39653d6409a13789359c8c615a3c196317b191a5d27d9bc1630be6d3db64134a055ddc0249ac4fecc2cdfff44f6677910469f0074d725c9e4bed64485c938977f5b1f8f95adb57b226065484dc5d36394897f228a240bb96953feeeba0c195cef9ba81631db6e8d2f505c704281469f36f2904e67cdeda7c90bd1a4b759630ad0609b2c6bf119779fdef0fd8ba93c7931e6e2567349791a2bdce86f5ac8a93c459ee6f74e0b8596bb7eab68abe013ceee2e9ecc56f7f4c64d16af85128f4bc08b0553865e9428c1a7cfb32bb3dfd6bf7961f6e485be5b20d5bed8b7e864903a162ba87aadcb69afec147e3b03eca21e72b5c1918057452af87086691db7f2fdb4965b791d3db78034dfbabe3335a5ed3aaba2ef599d4618eab8a6d3ba0ecd3a133ca82027ebc4c59ca7bc3501cdf4bafed4b37db0474cf87715d4efed38aa281adb4cfdcacf149e29b11e1ea85681ec7ded07ddc7ab24722674eda0d5fa5c9568aefd707b7a784499f97f3441865e02a97d5d04c86ad1c980951a52915fa5fbe11749e9eb87af4ea0d152bc871b782f8fa934556543ce6a21ecd1c17329ad343767620199d6ba1b74d7afa3580fdd4a274ce0f59356bc65ad4975b6b4ee065cabbe8d7212756175296c4d398752e7038715b751e2a3967a7ba841f7fcc28b525855823a88c85bbce822e77d5acc3f6e039af620fb758e63efb102b1203003ec72fe9ab2ce65f010166e2301a3a28a70e4fe0a27688512741a25de037213524c331ea40b567a8b9ce6ddb8c7abcbe0669b6cb60919325edffbdcfd3dea7ed2f59f3e5bc3e3bcb8492f68c65d0c81a0e9d14c1137c1bbade7af4abc2f14e7fe0110d4af955f6ca5bc4ce805d7c79712d275181802cac957bae3eef07ae31360f14db0445f7113dc88f87dc08fc87da92e344240ea19c1937d71eea1c708ade31c46d59efc2432bc5a3ffff87f0143a3bcd7b70000	6.2.0-61023	T
201803172252185_AddActivitiesTable	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1dd96e1cb9f13d40fea1314fc9423ba3636d3886b40bad6425c25ab2e2911779135a3d94d4704ff76c778f2d21d82fcb433e29bf10b24f5ec5a3ef3116060c4d932c168bc562b18aacfadf7ffe7bfcd3f33a70bea038f1a3f0647630df9f3928f4a2951f3e9eccb6e9c3f76f663ffdf8e73f1dbf5bad9f9d5fcb7a47a41e6e192627b3a734ddbc5d2c12ef09addd64bef6bd384aa28774ee45eb85bb8a1687fbfb7f5b1c1c2c100631c3b01ce7f8e3364cfd35ca7ee09f6751e8a14dba7583ab688582a4f88e4b961954e7da5da364e37ae864e6ff12465fe737049324c5a8a299731af82e466489828799e3866194ba2946f3eda7042dd3380a1f971bfcc10d6e5f3608d77b70830415e8bfadab9b8e64ff908c6451372c4179db248dd696000f8e0ad22cf8e68d083cab488789f70e13397d21a3ce0878323b0d93af289e397c576fcf8298542ba97b16c5689e4fc53c6fb3e764257b150b604e21fff69cb36d906e637412a26d1abbc19e73b3bd0f7cef17f4721b7d46e149b80d021a2b8c172e633ee04f3771b44171faf2113d14b85eae66ce826db7e01b56cda836f9382ec3f4e870e65ce3ceddfb0055934e8d7999e251fe1d85287653b4ba71d314c521818132b209bd737d619ec5ec97961d6246c34b66e65cb9cfef51f8983e9dccf09f33e7c27f46abf24b81c4a7d0c72b0c374ae32d9220a9e9384604e173fc5fd939f9fb16af276b589f362b1896bae93fb7282174d4d35c0de774b3c1eb34aec148287970f8a6134a5ebb5ffcc76cf6e548cc9c8f28c8ca93277f934b94795176972f8404231247eb8f5150372b8bee6eddf811119688e4e5cb681b7b1668953496e25516ca10e3cb04cc840a32d48e17b50451cb95927c5682256fb46b92a547f6e4fabcf0e324257f2aba7eb5df47cfefdd913ac622248e14bd1ebe7a65d4adba172cf457845bf36e7e7e491b5028f28a4dba2d85d4fd9ca307172f834b2f0aafb7ebfb9658bf5bbb7ea024efeb0e70ce7ac1dbe3831faf51b56e7e8eb0e071436b9c6fdc24f91ac5ab7fb8c953079badbab325f2b631966dcbd45d6f7aefede6290a113bad43f4d5d9d4dc7e8d2e5c0fab51ef42d2aa353cbca83e47dbf45d9829249f52cf5627a9007482cea9e7a124b9c0cc8c5667d1b6d6f59a293a64b3d30855d3f5d75ccff152ff8b9ffa2891ab3a79f1cb5db5a153ba0e57262a3b7c055b6da7d263da296165b7901256e26d8ad659e0fa6b3556651511a9bc04c4a928b645e9220a82e82be61e395ab7d1c6f7aa3a77fc4c4a8a85c994d5b19dcff7d1a31faa0957561109979780842b8a6d0957aabd6aaca85a22625521885b5dc3163d02478d5a5143442b2b0051ca4b6dd1c998408d4f594544282f01312a8a6528991f3b0a896379ee285aeddac1a37f93469747718de680c1579dd03b4f5e60ab87105e6a6b87e8cc9e916d336da1d41a8fad85a7998dc366e3179634a419345ad425b31248d9f6a85add57952df434d95ca3745eb69ee7702f620c139f1e3ecf05b07b8e71e35a441c9a8a88a383fb87a337af5ebbaba3d73fa0a357dfa2b818cc6c2753c81809d2d7a129ebe95737d876dd95f16a60f530bb8d8e6d3bfc7657f2c7425fb592e0b6fc3e160f36db72d49a16accbdfd10a3fa7c85365722d9eae60abc2831b46832306bf6da88e21cdd74a8325f28722c8f7358aedf71c255eec6f3456d583fdfd4676559d5ad4f29803797eb8535097077c53a1205d7732a9d1cd21baf22c8934e38a60a794826ac6d2a0f69dd90884b2d51f3241d8f1fc3450db2ecd5c3393920a93f4452bcca0563e5f7ee1834ee12ebde486b633485e8ad6b50e8c55162209a45917f62afa0c9a1930bb3fda6660a77fb4cdd0c49fbff89923d6e0845056c6e08deacb0f1f7a91ca6136f4b98219e6d09d0f23fb1a2d17b21abb5f2d04eaf4178bc5399a0c68978ed125be13e3b8eeb96d37386d576e468ded46870f8cf47cdf15d56a6d432c15140e49153b9d63bb9678c8726be9657211b88ff50de8265e3352def27084a701ef70c10b9e36faf0c2d2fe0a91ab2acc71b8b0ef64f658cc5ac274314d721db73e0416ad0e35ad92cf4293237593f7fe6754deab2e5afc20ce553e2bf4c7d324893c3fa33b3759951386edf55db872741e19d1a18617069e137f836701ff3a997d278c4601b7d2d229b8e5198005bb3f9f8b1382050b8a093bbbe43a588239c20f53510af9a1e76fdc408304d7ce507e11ea573df025e768436e0286a986ae265d5347471183aa234eb6ea0874bca0b844c33cdc0519708ea1db32da2916e717862a639c628de8d8b119d3c8473504cfc847be4b2c53dc5ed2cd2d7f95c98061f8517d08cf518052e49095461ec89cb989e7aec49d174bcc95213e125693b8907be53a963203321d4b02938e476538c16c0d4d316cc3ae27b9d006066139d8d1c6e143796a7be138883003f01c440393ae01f7ee406c27184da169862da8f534d7eae1209c073fe419686785683200c7416337e99abecd352ed3153667ed04f306683dcba95479d0d5a611a0dff147173be1cedd0850ca53e87a407391aaeec0fc30d3e95a6e2ee7e5f80f25eae5d333791d83bb59ae5322f96be6632bb5dcbd7640a92d9c47bd2ab52c6506546a5912ec0cc3d58e4fdd1c4b5e107472f89678579b6c22adb84618db808c238c7f970ee1f9c30edd1473af3cc69656ecb3124058e5ce945eb98e21cb801cc78c7f6724954623851ef068994d695a06aecbf5a68e4a1c31106e2aaf8cc8d23276ee6999297c41c3af35984a032c389812269dcbbde2bdacbadc2b4762c3e016958f2873976501639e5389db0d0fa8f0bc25856b92e70802748952fe8658ed03e44c01c23a6401148e49b179b9b035ede967c42290caf3a48122986965d024b65c0d58f6f42283c99f2b4d008270b4cda91b7102845a2bb3a055f9685649abe2886001b6785aaa849acb170d501d2029106a3d097c463d4ba3aa414fd7f8456ee037ad46c0b2b6202f0c5ca514a86295f1029f1da90915f8b7ef1222a8bc7fd28d983771eaf05602910cbe42b6b3e19722021ebdcc9125c59b7365351a3be77ea260c8e45a6b2a8857fe453aa8fd2b661e166a1ca5dc531003748cf06068c45bd342bc052dd2426df43733fb5383a084b8821ca0b5be9785215c6b565041a6f31bd9a19bd38053f5b58cd57451f0afd08075a1324d1b1ba7cdd9dad81cdde386512a0ab0c49459494deca48d242667db04246689746754a0d816260460bd33b4df35228768ae33596b2d0851e8653011243628032b54a3c1b3962380150a843b23002c2755a61113e348231a0c202065d751c5d1eb6c25a6d6126a04d2a933356e74c40ee51dd9ea345e951d2ff2b8c2c587e3051080f8f8cadd6c8874af5b165f9c651e8df8ecfba57d9cde750e63c1721a6f3ba87a4aa3d87d445c290940bb425988c8733775ef5d72a1f76cb516aa31b607e0e05676c59a17c4b92b0f74657df23765e1a0a33297a608d13e5334bec0835a130b4ff62e515090c4860e8906ed066e2cb9807e1605db75089b9ae0d655185f1a44f5d1020e1d959781451798c36322f3d2f098027378f485081a9ceaa2040c8df29ad0c014ce14b2cab89917ac71028309a652965f8db819926756bc2c372d983033d4b21f6ea642c6d240a8cfe6b0ea20b034a8faab39a422aa2b33acfc93398c32662b0da4fc6631a62a6c2b33a6eaab39244960561aa4a4d81c7611a69586577cb2844145fa14805165e650d960ac344cb6c41c2217719506c915596049c7556590a40b1ac103282aaf61de83184995862e965a713c1f5395637dbeb8016c09ce7c99c5de22865d65f618b1d862574dca18accc965a7d9dccae4599a15b6e5da537a6c1de0536ed67f36aa254800b2a7bf0c62ca2ec8b0584f2ce320304bac80cc3e958f1aaa20b3224aabe5a6c6055444166df02222d6770c6590a12737abb2521ba14edd786018c7e16896c85d82f0f2a841e7356a93f5bc22a82e409c08aef936126de6adb8e933847b23d1be900f4cb05b6226ecc39eb64aa9aced0508b5b544d6c8f5a4cb824f64042154c666a5566779bd9adee51d84f30dcb49f39becde368314b2effd4ff2c7f03e61c89aba83b6d20bf34d34e1b0060c0672826de107b8452064982613241849863aa2a88120ccf6eb7990063408eaca67c91dd586ac7167210fdeeefe5bd471a0a741772b479eb72ae5ace93dd1c0db5d5f73d2facff4cbe3f57ae53934db8aa0c6db5529d8ab80681a333e76d152964343f8a47c854d7f6588117918db0523f548510e3bd9ed613cbbbc40dbc1fe0b416e5a6b32a77a137241ffca0c26a4ee52ef9e1184d3f9f82339baf528989e24bf5bb7266178e647d8a5dc1b39c579939a5fe70325bbe60e658cf4985f9f2b7e02cf0337f6959e1ca0dfd07ccd879d4a7d9e1fec12197a6773a29731749b26222f6827973d9291b20a29a4f68aa8ddb6b9b3e814d5c1b7e7163efc98dffb2769fffda45325ae29e4e3b4a462b836512c8574ce692915278797119aed0f3c9ecdf59b3b7cee5bfeeea967bce8718b3f25b67dff9bd7d56db92cc464854ad591cf8c07add043d94baa887e0ee9a24560313410a8952a59049d4fa967950bb82cba4399502cda269dbf13c9bd534f5c3978ce75b2636351872ab3ca64df164529902247c6d8ba53c73e9bd6f8f9e2c6b2928691b67266d0551927db42b789d9010ca2eda04169859b4e90623cf34da043530cb689355517bb26d779db265f679cfb94c3e85fe6f5b5c708ba9c16d3edcca6ab5f9487dcc3bab5b0dbceddba6da6bc2525ca6a32620006dac6562bd2630f8b47a86ba6a8be475df065fb760ead68aace4d0c127806bb56f8949de2cc07597c8ada2dcefbb916b6d6c2690c925034c8a6616873bcbfc63dfc682efe3d42349e5233f0266c97c3a597572e7eecececa2d9d83c9e4dc38f0bc8cae1075b57743d979da09e91dcb7ad3997de6464c6ad319ec3137a52e33d90cb6fd77932266ecdd9f4d1c63854dde74c86917fdca3b655e6d665020831ec1a0a0f0319b111df6de897561e7ad7ea206d35f47f78ed87a221a4fdd1f87a0c10f416db2d894319d6401c80cb2cb88c1981a26c069137a72c8b4343b15cfd4300f4debd8b745c03b1aca008966069976e801fd7427dd2c93cc88816b25d10481487703e48d193270a6eaad93c5463abd2c31e3e684a10cba3c1a83a482193e1bc0b798ef65cc042f436f608a973263ee6053cbd722bd963a31d6618fba32444c626aef0223292f8e4f590229ee134f66231b978d06dbc6ec79687499649352697cdd7a6c856840adba814a3489f3995952a4899ccf0a1f1c703e1b2005d2d0e733e8f5e94499c938e1515b1b8f95aeb57b2c60a5484dc5d26394ae68228224f7b80272a4ffec44438b11e011ec44a588d9716b7c86627c531234fad49187343a2bde724e9e89267dcc1a9381063b6459b3cfb8e7f48649c2a692176ccccd4d1372611a3bdce889bfc418ddfc9c16afe899370e8ac45ef94bec93d9ea9ebc1fccddf55046110178b9618ad0cb1229f864738dd22278bcae8bcae92ef65115493b01d32ee9d38699650d530f0dc8d3c3f7cd1b32848ef90ab25e750971a49dc2e9c7c02ef490eb439400bc2e92c187d353a866ab38b32b67aba8a39e2d204788aaef5c142abbceaba87b96e720803ad674aaebd0ac33ce4a0bcea48e5dcce794d5582069a2eb4f2e5106c903c70a422650bbb857c8ee19d10da17487d348f4663f48660ba233327437c0ee52b9590f0fdc3d80b0aeed07dd47e636315b8c72d0f26d4b1681b4fd707b4ace26cd8e341a2343d9d2b419d88070479d0c58ba1148026f753ffc32718f7ef8f2c84a2dd97bb881f79270aea9281b7255731ea40e52cab592dc8c260984e0ec6ed05d6790b31eba954c984086b856734b2bcdf2309add0d582bbe8d82d575a17549546a31b85ee7038705b75144bc96727ba841f79c054fcaac32461d84e52db2de8901018f171fb721098891ff3a4789ff588338c63043e431a6afaace65f8109516380ea3b20af778e50aa5eeca4dddd338f51f5c2fc5c524144eb66165f1184840a67bb4ba0c3f6cd3cd36c54346ebfb803969114b9eaaff2cb51f8bf3f187eca973d2c510309a3e8921f221fc79eb07ab0aef0bc90b1a00043111164fc4c95ca6e4a9f8e34b05e93a0a0d0115e4ab2c9bb768bd0930b0e443b874bfa026b861f67b8f1e5defa57e1a0c01d14f044bf6e373df7d8cdd7552c0a8dbe39f988757ebe71fff0faa9ce7fea0ca0000	6.2.0-61023	T
201803190932054_AddSeedActivitiesForSeedQuestionsAndAnswers	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1d5d6fdcb8f1bd40ff83b04fedc1b7eb8f4b9006eb3bf89cb8352e76dcac73e85b20af685b8856da93b4898de27e591ffa93fa174a4994c4afe187be3738f8c52b92c3e170381cce9033fffbcf7f973f3d6d02e70b8a133f0a4f6747f3c39983c275e4f9e1c3e96c97de7fff6af6d38f7ffed3f2adb779727e2deb9d64f570cb30399d3da6e9f6f56291ac1fd1c64de61b7f1d4749749fced7d166e17ad1e2f8f0f06f8ba3a305c220661896e32c3fecc2d4dfa0fc07fe791e856bb44d776e7015792848c8775cb2caa13ad7ee06255b778d4e67fe2f61f4757e936192a418553473ce02dfc588ac50703f73dc308c5237c568befe98a0551a47e1c36a8b3fb8c1edf316e17af76e902082feebbabae9480e8fb3912cea8625a8f52e49a38d25c0a313429a05dfbc11816715e930f1de6222a7cfd9a873029ececed6a9ff057f9a397c67afcf8338ab58d2f73c8ad1bc988c79d9eac0c9cb0e2a36c0dc92fd1d38e7bb20ddc5e83444bb34768303e7667717f8eb5fd0f36df41985a7e12e0868cc306eb88cf9803fddc4d116c5e9f307744ff0bdf466ce826db7e01b56cda836c5482ec3f4e478e65ce3ceddbb0055134f8d7a95e271fe1d85287653e4ddb8698ae230838172d209bd737d9d6db798c7e2ba4bcc6e78e1cc9c2bf7e91d0a1fd247bca48e5fcd9c0bff0979e51782c6c7d0c7eb0c374ae31dd2f59481af3ac17c5e4d4a51208c51032ddafa6b3da1d440feb9434946c4b670cec2e42b4dc36650dee009bcc522a58452ffd601ba76bff80f393bc86777e67c40415e9e3cfadb92fc45d927320d3e4af024c7d1e64314d42da9d24fb76efc80528c5d04565945bb78cde1b75cd48b58bdb44b64ad5676d168df16768b5566c955177e9ca4d9bf8aae5f1cf6d1f33b77a48ef1f28b2345afc72f5e7420cfb0ccf5326e2dbaf9f9396d40a1684df6c9b614d2481674efe26570b98ec2ebdde6ae25d66f37ae1f28c9fbb2039cf35eb05675efc71b54ad9b9f232c7adcd01ae71b3749be46b1f70f377954a08effed00f5155aef622cdb56a9bbd9f6dedbcd631422765a87e8abb3a9b9fd1a5db86bacc5bc0db356ade1e145f539daa56f432fdb403fa66b713f3504d0093a67eb354a920bccccc83b8ff0a1a19d92906d761aa16abafe9a6b1594c6d08562516a0d0ac5a2d43d8c51cc35320d7e651d097245118c1929b745eb3c70fd8d1aabb28a88545102e2448a6d51ba888220fa8a79488e56ae6757753e150a5a8d9ba458d0136575648aa20acb77d1831faa09575611095794808423c5b6842b0f0f6aaca85a22625521885b5dc316bd0c8e1a355243442b2f00512a4a6dd1c999408d4f594544a828013122c53294cc0f1ff98ab63c7be46df6ede8d1bf4d01ab0629aa37ba16da88e5e6781ea30ce16ca3b73f44f31bedd683610d6456e8d036d3af0941bf9382c6036ea7b595be52bcca4219627c99809950a19551a3c6d346b294adfe902dc269c14f03b5f26b76b6b7b5cda1641dfb5bcd61fde8f0b08be3fa34d6bd4283b65a5ffcae0d2ec02e2592a1da0549255131eb40cfa9062e2a3a5c114cb32e549d1c869d3cca9bfc218cf8be46b16ef62b8a74abaea50a0f2d388eeffb39bc12c6074faff94fddf1b5a8d4545d692d19406d4541413bc9500db58188a8da0e2f2bca1d73a1af5af90e6d65cb60bb7227ce4ef51ed5dd82e1372be5aa32c511dce11b18a434083246ab46eba6dc2f3248b9194eb574aeaacb0f67c9f61aa5f3b2f5bc807b1163985fa3f8f35c007be01837ae57deb1e9ca3b39babb3f79f5e2a5eb9dbcfc019dbcf81677ecb156703e7dcc0d88be5c34794fbfbac1aeebae1aad86dcb6dafd6ac8c14e7f35e468e2cf5ffcdc536cb03595953178a3faf25d4fbfe638cc865e0ecc3087ee7c1819d068b964fb53f7ab25833afdc562a1c06503da27fdadc477621cd73db7ed07a7edcbd5adb1fdfcf0799f9eef4fa45aad838ba5820a2ea962a781ef36924bc1858275995c04ee437d4bbac945e1acbce519164f03dee182673c6db476cbd2fe0a657769186b063974e52a1c662d61ba9826850db576329056c79a569e273439513779e77f46a58f94b4f8419cab6256e88f674912adfd9ceeac2189be1ec2f6fb36f41cfd5d11ea1e71690fbec2d3e26ff144e0093c9d1dcee722f194b02bdb8a78479907fe9d00198b1714674ced66b7d612cc177e988ab2c80fd7fed60db403e45a1acab16c16aa3ef89237689b5d590c532d054c3aa79c14220e55579c94d51169b9a0f845cd46820f029a68d821514f73bd1ad8693ee249b17c1fbe41014a9193512d7b2071ee266bd713a52a5e0d9e314a32ce238bad17be83683200d7416337e99a76ac8fc27452fb1834cb6a63593dd3bcd55537e39a1e24bc44b6941e79bb1117aad01f801355f363d23d60461e8715f31dd0904f58a3680f8cc85e03d4eed493e3441affe119919e1e93dea7b10d13d79576cbe33ddcfa4d58c578a07b4c23fcbee37557e301f3779eb4fa25ac7580cbc1406b1d4971004635a4b66aaf368cadaa7257bb7573cbdff31e567e82f848584de2f7ea95eb58ca0cc8742c09262f94b92bf1ba09e6efc78fcd70dc857c80e1886ba9578663293320c3b124d81b86abafb9e8e658f2f4a1938d5172b7af8986d18a6b84b10dc838c2f8f769832c5ea4e8a6987b9e32b6b462dfc300c2aa70b5f4ca750c5906e43866fc7b23a934c715e8e59196d9548715e82e646f6715899b06c24de5b311595ac6ce3d2d3385a768f8b506536980050753c2a473b9cfbc975557f8ecb25760b845e541ca9d69f9d3b0a754e294c303227eb984382e798ec880ae502a79845c3b0905b78db0185928c47729022857b7a67df5004284408ee11a00d4c5600144adae688094d7860508449e9834a7af52cbe15096410d40e15c288329393c5a802d5ffb2ac19223820558f2265609b5902f1aa03a405220d47aa2584cf29c9eaa083fbae717ba915fb51a47b536048161e443a5e0d088f3829f1db10135c4a747222dd4ae41b9fd9037d351f8534b54410bd0a74753a2c4b93519e417b54552e81d562a3b3eebb2a20622480c0561944e2a1e660fb429e4b89634a203c5d885d20d6118a7896e19b6593925a1150b47a61d1b99f39b2f1b4e29ee833184e7ca0a31aa951c907dbf99001d4266f01146e0d1cb0cd526a6ea4663e7cccb140c991ad119154a0502a682cc7a6a623f6d4405cee60950a144ba332a508b14260460d533b4eb35228768c633912c2d0841f435980812db948175aad1e0598b12c00a04e1ce0800ef0a2a938989d1a4110d06d80e649758c5d1eb6c28a656146a04d2a933357a74c40ee5cddaea945e952d1745c462f261b900421b2fafdced365372ea96e48bb32ae21c9f7fbfb28f00bc29602c584ee36d0a554f6914bb0f882bcd42d078288f7cf9c64ddd3b37bb067cee6d846a8c4d0238d0955d09660771facab35ed924fb9f327ed0219f6b138568bc21cd2ff0c83699f9277ff3261ea8244d9d2cdcb41bb8b1e4f6fa7914ec36216c89825b53ee011a88c26b00c32aae68d3608a2f1610ca6b570c10e82e160c87be4c4883525d3254d0a80a01cc90a8fa6a0ea98e32444382620fe570161cc708263e813505fb2bcbec464b01128656ab80d8d91a2c02a8653f6b800aa34b03a13e9bc3aa03e3d2a0eaafe69048a45b6658c5277318651c5b1a48f9cd624c55285b664cd5570bfe1783d5320b412c36874d42d7d2f0c8274b1854f453011855660e950d504bc3644bcc2172516869905c91059674ac590649baa0113c80a2f21a16bb85105d96d93684522b8ee7e3cc72accf1737802dc1992fb3d8a3c450b4cc6625169bc3ce64b128d3eaaf93d9b560cb8ed5be5578771a6c5b40c37e76ad2a12230da2fa6801870eacc8c0a20b2cb8850eaec8300c5d309a2ed740df1d899b55a6191b7eae5c8df61c0d37ed87a76f8b08808c182f3ed9e818540c2d56bba00abe498e01ad3836ec22f52319f00ad0ae1f4611b7235bf5ba299b8c39b594f3a98339ae7dfe0d275b0140a54af033dfc0d06069261869ce243e8e76d3265eabb09f390318fd2cd96ea69e0ab6c4e82af5674b58249c92008c7c9f243381deaba6cc545ca669c74c000cf818c44429624f41cad04a304c26f41073d254855e82e1d9b1ec041803726435e58bfc26533bb69083e8574894f7216928d01dc9d1e6adcbb96a394f76733494e6d6f7bcb0fe33f9d9ab729d9a1cb0aacad0310abca2051c79396fab4821a3f951bc65a7bab6c70abca06c84953ade038418eff5b49e58de256ee0c000a795949bceaadc85de907cf0430bab3995bbe4876334fd7c0ace6cbe4a2526c897ea77e5cc268e647df25ec1b35c549939a5fe703a5b3d63e6d8ccb30af3d56fc179e0e7e6b6b2c2951bfaf798b18b5851b3e3c3a3632e01f07492f12e92c463c2312b32f2b293364024363fa3aa36206cdba42be117375e3fbab2dc4097a1879e4e67ffce9bbe762efff5a96a7de0bc8ff15cbf760e9ddfc55871354eb69973f331b70b1fdd048498d2a609143e4f6e13187c96dccc5e9cea13fc5867a11d9c9b6b3e83b9a559c65729e42c3941cb84ae5dc165f2b54a81e6595dec160d9b9e35f5c3e726dcc66768351872ab84ac4df16472b202247c698ba53c05eb9d6f8f9e2cfd6a89e55f36eed35f6d5193a6586d05519246b52b789d90104a93da0416982255264c4d062b4f99da0435305d6a935551bb9f6db7f2b265fef9c0b94c3e86fe6f3b5c708ba9c1ede8dcca6ab5f948bcc37bab49716900c1d5d434b59fe1ce2f610b31b55f53c607f422034db16ec9aa8ad35657db30b7dc51bcb7ec7d4b67a233d15a9a6677922b86797e273b66dd235e9145d6dc5746e94365ee8e4dba4b0e55b1cceffb91bfc98afb5bb3bed9b1dc0013d2cc62d76891c7e8db5881d39a77211750ab738698efc7025ca7397ddaadff3dcb95d399b5e6464c85d319ec31f9becbfc3783ed2cdd2496195bc0b0e966acb0299a0e39eda25f79af8cadcdcc0bd9a047302f287ccc664487bd77625dd879ab9fa8c154a3d14ff4b67e89c653f7877e3db87edd45ee9b2e32dd54c1d0a411d2fa0c4a39481c4af821b5d897e246fe34b3d68c99a6a60c6127896ad76b569a41b8067ac065b3910dc434f6596706cefe02c5b46a955da479228fe173767c63f9622cf69e6f948f06dcbf1a70d234b630c51dc5896d64ecb947868849e0e57dd8d694b7882728954cee0b4f66731b978d06dbdaec7968740d4919b6b1e9c6a638510dac180f799a32d78bc73e4ba9a2554e4a999144baa670193889d29091ea556f5027a6d5a8827e4e969988270e60a60112240dcd4cd01bd4893293713aa4b61b929592bd7f2c60a5414f655b324a6634114152f85d0139d27feea2a1c508f01476a252c4ec9c3d3e43311e2a091a7d1e8e86d490152f3a27cf44933e5f8fc940839daeadd9675c034dc3146253c91a36e6e6a609bc308d1d6ef4b46062a46e7e4ec95bfaea8150e9b8ae9dee824fbb78937d3af3eeb2778585e35e918648e8a3dc37c52eca12690fc9f61aa52492bcae0b622f127b2005d20e805c203cf05af317c0d745b20ee0cc0a7c1744160bf0c97719702070bf1432e5df9177415500fb82f3f0e8339699252c53730190b344d53739b32bfb2675d47d039942547d17a250d9755145ddb33c1301d4b1a6535d87669d71e6797051e898d77c79b01a0b244674fdc945c9c059e2d86c44f2bc886c3b4e42b3390b845d7c2a29e0a42966d4036564381ddeb7fd20fb4cf026a653510e532e9065211e3b1f76c16b6d73b73565e421070e2554d3266903620475c2e052b9298956d5fdf0cb6c37fae1cbc311b564f9e106ce7b2ebbc849672fb17b14649c97ae83ac73d6c303953b20d8697783ee2ec95cab41335a251094b3bb41779d53ce7ae856026f0239e35acd2dad40cb036b763760edde6414beae0b812551afc5707b9d0f1cde958c62e4b5dc94861a74cf79f1a4cc2a63d44158de220f9e182270b9f8b00bb3b021c5af3728f11f6a104b0c33446bc60c56d5b90cefa3d21ac7615456e19eb35ca1d4f5dcd43d8b53ffde5da7b8380b9593ab9ff9fbdf2c60d31df22ec3f7bb74bb4bf190d1e62e604e5c99554fd57f9eec8fc579f93e8f32907431048ca69f455a791ffebcf303afc2fb42f2a6060091990bc9cbf06c2ed3ec85f8c37305e93a0a0d0111f25556ce5bb4d9061858f23e5cb95f5013dc30fbbd430feefab97e2c0c01d14f044bf6e51bdf7d88dd4d4260d4edf14fccc3dee6e9c7ff032f16b37d0ccb0000	6.2.0-61023	T
201803190957172_AddFollowTopicSeedData	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1d5d6fdcb8f1bd40ff83b04fedc1b7eb8f4b9006eb3bf89cb8352e76dcac73e85b20af685b8856da93b4898de27e591ffa93fa174a4994c4afe187be3738f8c52b92c3e170381cce9033fffbcf7f973f3d6d02e70b8a133f0a4f6747f3c39983c275e4f9e1c3e96c97de7fff6af6d38f7ffed3f2adb779727e2deb9d64f570cb30399d3da6e9f6f56291ac1fd1c64de61b7f1d4749749fced7d166e17ad1e2f8f0f06f8ba3a305c220661896e32c3fecc2d4dfa0fc07fe791e856bb44d776e7015792848c8775cb2caa13ad7ee06255b778d4e67fe2f61f4757e936192a418553473ce02dfc588ac50703f73dc308c5237c568befe98a0551a47e1c36a8b3fb8c1edf316e17af76e902082feebbabae9480e8fb3912cea8625a8f52e49a38d25c0a313429a05dfbc11816715e930f1de6222a7cfd9a873029ececed6a9ff057f9a397c67afcf8338ab58d2f73c8ad1bc988c79d9eac0c9cb0e2a36c0dc92fd1d38e7bb20ddc5e83444bb34768303e7667717f8eb5fd0f36df41985a7e12e0868cc306eb88cf9803fddc4d116c5e9f307744ff0bdf466ce826db7e01b56cda836c5482ec3f4e478e65ce3ceddbb0055134f8d7a95e271fe1d85287653e4ddb8698ae230838172d209bd737d9d6db798c7e2ba4bcc6e78e1cc9c2bf7e91d0a1fd247bca48e5fcd9c0bff0979e51782c6c7d0c7eb0c374ae31dd2f59481af3ac17c5e4d4a51208c51032ddafa6b3da1d440feb9434946c4b670cec2e42b4dc36650dee009bcc522a58452ffd601ba76bff80f393bc86777e67c40415e9e3cfadb92fc45d927320d3e4af024c7d1e64314d42da9d24fb76efc80528c5d04565945bb78cde1b75cd48b58bdb44b64ad5676d168df16768b5566c955177e9ca4d9bf8aae5f1cf6d1f33b77a48ef1f28b2345afc72f5e7420cfb0ccf5326e2dbaf9f9396d40a1684df6c9b614d2481674efe26570b98ec2ebdde6ae25d66f37ae1f28c9fbb2039cf35eb05675efc71b54ad9b9f232c7adcd01ae71b3749be46b1f70f377954a08effed00f5155aef622cdb56a9bbd9f6dedbcd631422765a87e8abb3a9b9fd1a5db86bacc5bc0db356ade1e145f539daa56f432fdb403fa66b713f3504d0093a67eb354a920bccccc83b8ff0a1a19d92906d761aa16abafe9a6b1594c6d08562516a0d0ac5a2d43d8c51cc35320d7e651d097245118c1929b745eb3c70fd8d1aabb28a88545102e2448a6d51ba888220fa8a79488e56ae6757753e150a5a8d9ba458d0136575648aa20acb77d1831faa09575611095794808423c5b6842b0f0f6aaca85a22625521885b5dc316bd0c8e1a355243442b2f00512a4a6dd1c999408d4f594544a828013122c53294cc0f1ff98ab63c7be46df6ede8d1bf4d01ab0629aa37ba16da88e5e6781ea30ce16ca3b73f44f31bedd683610d6456e8d036d3af0941bf9382c6036ea7b595be52bcca4219627c99809950a19551a3c6d346b294adfe902dc269c14f03b5f26b76b6b7b5cda1641dfb5bcd61fde8f0b08be3fa34d6bd4283b65a5ffcae0d2ec02e2592a1da0549255131eb40cfa9062e2a3a5c114cb32e549d1c869d3cca9bfc218cf8be46b16ef62b8a74abaea50a0f2d388eeffb39bc12c6074faff94fddf1b5a8d4545d692d19406d4541413bc9500db58188a8da0e2f2bca1d73a1af5af90e6d65cb60bb7227ce4ef51ed5dd82e1372be5aa32c511dce11b18a434083246ab46eba6dc2f3248b9194eb574aeaacb0f67c9f61aa5f3b2f5bc807b1163985fa3f8f35c007be01837ae57deb1e9ca3b39babb3f79f5e2a5eb9dbcfc019dbcf81677ecb156703e7dcc0d88be5c34794fbfbac1aeebae1aad86dcb6dafd6ac8c14e7f35e468e2cf5ffcdc536cb03595953178a3faf25d4fbfe638cc865e0ecc3087ee7c1819d068b964fb53f7ab25833afdc562a1c06503da27fdadc477621cd73db7ed07a7edcbd5adb1fdfcf0799f9eef4fa45aad838ba5820a2ea962a781ef36924bc1858275995c04ee437d4bbac945e1acbce519164f03dee182673c6db476cbd2fe0a657769186b063974e52a1c662d61ba9826850db576329056c79a569e273439513779e77f46a58f94b4f8419cab6256e88f674912adfd9ceeac2189be1ec2f6fb36f41cfd5d11ea1e71690fbec2d3e26ff144e0093c9d1dcee722f194b02bdb8a78479907fe9d00198b1714674ced66b7d612cc177e988ab2c80fd7fed60db403e45a1acab16c16aa3ef89237689b5d590c532d054c3aa79c14220e55579c94d51169b9a0f845cd46820f029a68d821514f73bd1ad8693ee249b17c1fbe41014a9193512d7b2071ee266bd713a52a5e0d9e314a32ce238bad17be83683200d7416337e99a76ac8fc27452fb1834cb6a63593dd3bcd55537e39a1e24bc44b6941e79bb1117aad01f801355f363d23d60461e8715f31dd0904f58a3680f8cc85e03d4eed493e3441affe119919e1e93dea7b10d13d79576cbe33ddcfa4d58c578a07b4c23fcbee37557e301f3779eb4fa25ac7580cbc1406b1d4971004635a4b66aaf368cadaa7257bb7573cbdff31e567e82f848584de2f7ea95eb58ca0cc8742c09262f94b92bf1ba09e6efc78fcd70dc857c80e1886ba9578663293320c3b124d81b86abafb9e8e658f2f4a1938d5172b7af8986d18a6b84b10dc838c2f8f769832c5ea4e8a6987b9e32b6b462dfc300c2aa70b5f4ca750c5906e43866fc7b23a934c715e8e59196d9548715e82e646f6715899b06c24de5b311595ac6ce3d2d3385a768f8b506536980050753c2a473b9cfbc975557f8ecb25760b845e541ca9d69f9d3b0a754e294c303227eb984382e798ec880ae502a79845c3b0905b78db0185928c47729022857b7a67df5004284408ee11a00d4c5600144adae688094d7860508449e9834a7af52cbe15096410d40e15c288329393c5a802d5ffb2ac19223820558f2265609b5902f1aa03a405220d47aa2584cf29c9eaa083fbae717ba915fb51a47b536048161e443a5e0d088f3829f1db10135c4a747222dd4ae41b9fd9037d351f8534b54410bd0a74753a2c4b93519e417b54552e81d562a3b3eebb2a20622480c0561944e2a1e660fb429e4b89634a203c5d885d20d6118a7896e19b6593925a1150b47a61d1b99f39b2f1b4e29ee833184e7ca0a31aa951c907dbf99001d4266f01146e0d1cb0cd526a6ea4663e7cccb140c991ad119154a0502a682cc7a6a623f6d4405cee60950a144ba332a508b14260460d533b4eb35228768c633912c2d0841f435980812db948175aad1e0598b12c00a04e1ce0800ef0a2a938989d1a4110d06d80e649758c5d1eb6c28a656146a04d2a933357a74c40ee5cddaea945e952d1745c462f261b900421b2fafdced365372ea96e48bb32ae21c9f7fbfb28f00bc29602c584ee36d0a554f6914bb0f882bcd42d078288f7cf9c64ddd3b37bb067cee6d846a8c4d0238d0955d09660771facab35ed924fb9f327ed0219f6b138568bc21cd2ff0c83699f9277ff3261ea8244d9d2cdcb41bb8b1e4f6fa7914ec36216c89825b53ee011a88c26b00c32aae68d3608a2f1610ca6b570c10e82e160c87be4c4883525d3254d0a80a01cc90a8fa6a0ea98e32444382620fe570161cc708263e813505fb2bcbec464b01128656ab80d8d91a2c02a8653f6b800aa34b03a13e9bc3aa03e3d2a0eaafe69048a45b6658c5277318651c5b1a48f9cd624c55285b664cd5570bfe1783d5320b412c36874d42d7d2f0c8274b1854f453011855660e950d504bc3644bcc2172516869905c91059674ac590649baa0113c80a2f21a16bb85105d96d93684522b8ee7e3cc72accf1737802dc1992fb3d8a3c450b4cc6625169bc3ce64b128d3eaaf93d9b560cb8ed5be5578771a6c5b40c37e76ad2a12230da2fa6801870eacc8c0a20b2cb8850eaec8300c5d309a2ed740df1d899b55a6191b7eae5c8df61c0d37ed87a76f8b08808c182f3ed9e818540c2d56bba00abe498e01ad3836ec22f52319f00ad0ae1f4611b7235bf5ba299b8c39b594f3a98339ae7dfe0d275b0140a54af033dfc0d06069261869ce243e8e76d3265eabb09f390318fd2cd96ea69e0ab6c4e82af5674b58249c92008c7c9f243381deaba6cc545ca669c74c000cf818c44429624f41cad04a304c26f41073d254855e82e1d9b1ec041803726435e58bfc26533bb69083e8574894f7216928d01dc9d1e6adcbb96a394f76733494e6d6f7bcb0fe33f9d9ab729d9a1cb0aacad0310abca2051c79396fab4821a3f951bc65a7bab6c70abca06c84953ade038418eff5b49e58de256ee0c000a795949bceaadc85de907cf0430bab3995bbe4876334fd7c0ace6cbe4a2526c897ea77e5cc268e647df25ec1b35c549939a5fe703a5b3d63e6d8ccb30af3d56fc179e0e7e6b6b2c2951bfaf798b18b5851b3e3c3a3632e01f07492f12e92c463c2312b32f2b293364024363fa3aa36206cdba42be117375e3fbab2dc4097a1879e4e67ffce9bbe762efff5a96a7de0bc8ff15cbf760e9ddfc55871354eb69973f331b70b1fdd048498d2a609143e4f6e13187c96dccc5e9cea13fc5867a11d9c9b6b3e83b9a559c65729e42c3941cb84ae5dc165f2b54a81e6595dec160d9b9e35f5c3e726dcc66768351872ab84ac4df16472b202247c698ba53c05eb9d6f8f9e2cfd6a89e55f36eed35f6d5193a6586d05519246b52b789d90104a93da0416982255264c4d062b4f99da0435305d6a935551bb9f6db7f2b265fef9c0b94c3e86fe6f3b5c708ba9c1ede8dcca6ab5f948bcc37bab49716900c1d5d434b59fe1ce2f610b31b55f53c607f422034db16ec9aa8ad35657db30b7dc51bcb7ec7d4b67a233d15a9a6677922b86797e273b66dd235e9145d6dc5746e94365ee8e4dba4b0e55b1cceffb91bfc98afb5bb3bed9b1dc0013d2cc62d76891c7e8db5881d39a77211750ab738698efc7025ca7397ddaadff3dcb95d399b5e6464c85d319ec31f9becbfc3783ed2cdd2496195bc0b0e966acb0299a0e39eda25f79af8cadcdcc0bd9a047302f287ccc664487bd77625dd879ab9fa8c154a3d14ff4b67e89c653f7877e3db87edd45ee9b2e32dd54c1d0a411d2fa0c4a39481c4af821b5d897e246fe34b3d68c99a6a60c6127896ad76b569a41b8067ac065b3910dc434f6596706cefe02c5b46a955da479228fe173767c63f9622cf69e6f948f06dcbf1a70d234b630c51dc5896d64ecb947868849e0e57dd8d694b7882728954cee0b4f66731b978d06dbdaec7968740d4919b6b1e9c6a638510dac180f799a32d78bc73e4ba9a2554e4a999144baa670193889d29091ea556f5027a6d5a8827e4e969988270e60a60112240dcd4cd01bd4893293713aa4b61b929592bd7f2c60a5414f655b324a6634114152f85d0139d27feea2a1c508f01476a252c4ec9c3d3e43311e2a091a7d1e8e86d490152f3a27cf44933e5f8fc940839daeadd9675c034dc3146253c91a36e6e6a609bc308d1d6ef4b46062a46e7e4ec95bfaea8150e9b8ae9dee824fbb78937d3af3eeb2778585e35e918648e8a3dc37c52eca12690fc9f61aa52492bcae0b622f127b2005d20e805c203cf05af317c0d745b20ee0cc0a7c1744160bf0c97719702070bf1432e5df9177415500fb82f3f0e8339699252c53730190b344d53739b32bfb2675d47d039942547d17a250d9755145ddb33c1301d4b1a6535d87669d71e6797051e898d77c79b01a0b244674fdc945c9c059e2d86c44f2bc886c3b4e42b3390b845d7c2a29e0a42966d4036564381ddeb7fd20fb4cf026a653510e532e9065211e3b1f76c16b6d73b73565e421070e2554d3266903620475c2e052b9298956d5fdf0cb6c37fae1cbc311b564f9e106ce7b2ebbc849672fb17b14649c97ae83ac73d6c303953b20d8697783ee2ec95cab41335a251094b3bb41779d53ce7ae856026f0239e35acd2dad40cb036b763760edde6414beae0b812551afc5707b9d0f1cde958c62e4b5dc94861a74cf79f1a4cc2a63d44158de220f9e182270b9f8b00bb3b021c5af3728f11f6a104b0c33446bc60c56d5b90cefa3d21ac7615456e19eb35ca1d4f5dcd43d8b53ffde5da7b8380b9593ab9ff9fbdf2c60d31df22ec3f7bb74bb4bf190d1e62e604e5c99554fd57f9eec8fc579f93e8f32907431048ca69f455a791ffebcf303afc2fb42f2a6060091990bc9cbf06c2ed3ec85f8c37305e93a0a0d0111f25556ce5bb4d9061858f23e5cb95f5013dc30fbbd430feefab97e2c0c01d14f044bf6e51bdf7d88dd4d4260d4edf14fccc3dee6e9c7ff032f16b37d0ccb0000	6.2.0-61023	T
201809080527425_AddCommentsTable	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1d5d6fdcb8f1bd40ffc3629fda22e7f5c725b81af61d7c4edc1a17276eec1cfa16c8bbb423442bed49dac44671bfac0ffd49fd0ba52452e2d7f043a23e360dfce215c9e17066381c0e879cfffefb3f273f3daea3d967946661129fce0ff6f6e733142f9355183f9cceb7f9fd773fcc7ffaf18f7f3879b55a3fce7ea5f58e8a7ab8659c9dce3fe6f9e678b1c8961fd13ac8f6d6e1324db2e43edf5b26eb45b04a1687fbfb7f5d1c1c2c100631c7b066b39377db380fd7a8fc817f9e27f1126df26d105d252b1465e43b2eb929a1cede046b946d82253a9d87bfc4c997bdeb02932cc7a8a2f9ec2c0a038cc80d8aeee7b3208e933cc8319ac7ef337493a749fc70b3c11f82e8f6698370bdfb20ca1041ffb8a96e3b92fdc362248ba62105b5dc6679b2760478704448b3109bb722f0bc261d26de2b4ce4fca9187549c0d3f9d9320f3fe34ff399d8d9f179941615297dcf9314ed55ccd8a3ad9ecdcab267b518606929fe9ecdceb751be4dd1698cb6791a44cf66d7dbbb285cfe829e6e934f283e8db751c4628671c365dc07fce93a4d3628cd9fdea17b82efe56a3e5bf0ed1662c3ba19d3a61ac9659c1f1dce676f70e7c15d846ac633a3bec9f138ff86629406395a5d07798ed2b880814ad249bd0b7d9d6d3658c6d2a64b2c6e78e2cc6757c1e36b143fe41ff1943afc613ebb081fd18a7e2168bc8f433ccf70a33cdd22534f05f8ba132ce73553aa02698c0668c9265c9a09a507f28f2dca0a2276857316675f581ab683f21233f016ab140aa5f96d02f426f81c3e94e2a0e6ee7cf60e456579f631dc50f257651f081b42946126a7c9fa5d12352d99d20fb741fa80728c5d0256b949b6e952c0ef64d14c62fdd4a6c83acdecaad1ae4dec0eb3cc51aa2ec234cb8b7f355d3fdfefa3e7d7c1481de3e997269a5e0f9f3ff7a0cfb0ce5d15d25a75f3f353de8242c992ac935d2964d02ce83ec0d3e07299c46fb6ebbb8e58bf5a0761a425ef0b0f3897bd60abea3e4cd7a89e373f2758f504b133ced741967d49d2d5df83eca30675fcaf07d46fd0729b62dd769307eb4defbd5d7f4c62c4b37588bebcb1e6f64b72112cb115f32a2e5a75868727d5a7649bbf8a57c502fa3e5fcaeba925002fe89c2d9728cb2eb030a3d57982370ddd8c8462b1332855dbf9d7deaa602c061f8605b51a348605b53dac512c2d32037eb48e02b9aa08c68c94bba2751e05e15a8f15ad222355958038916257942e92284abe601952a355dad9759d0f9581d6e0a62896ec44551d95a1a8c3f275f210c67ac2d12a32e1aa129070a4d8957074f3a0c78aa92523561782b835355cd12be0e851233564b4ca0210a5aad4159d5208f4f8d02a3242550988112956a164bff92867b4e3dea36cb36b5b8ffe7d0ad834c851b3d075b0461c17c7f3141508170bbdfb265a5c68372b18d6406e058fbe997e5d08e69514741e082badf54a9aacd758c6d41a85147ea093ba418b2f91b0128a5d91a28c5722450b55d412cb24c4a40a9d3c2d64986eda8e34faa6eebe4a75e7c785399c37f91dda4458ae88503ae36dd83874d32aa29d02281d1f1ab8064deb2890229a16c48a5a51ae0626c7012d7662551949be0688ab50ad93c5d7286b172d485b7d5383921f27cc23bd5bc2ceebea7a6a82b2651a6e0c6ed483fd7d1f8ed46958641adf86939121ce32d00af1692b5a6e88217b51de327bd881d60397b7a042114c331f9bd012869b3e2a9b7c5346625fa39c3bf5ab8a4cb3aea373059a7082dcf7e35624820ffa15cb9f26c76255a9ed9eadb36600b76c1a0aba69867aa82d5444dd76785d4157cc85b96a1dd5e1aa5b065b95bd84a1e8d7287f13465cacb4b3ca164770856f7154604010dc9758cf1bba5e1490ca0312ddd4b9aac3d2ceb2cd1b94efd1d67b15dc8b14c3fc92a49ff624b0cf66d68d9b9977683bf38e0eeeee8f7e78fe22581dbdf81e1d3dff1a57ecb16670c93e2e36adafc3f3b2a75f8368ebbbab56b3a13cf5f23f1b4ab0d39f0d259af8f3e7b08ce1b1589a68650cdeaabe7ad533cf3901b3a1a70337cca13b1f4607b49a2ec5fae47fb61450a73f591c0cb86240bb64bf517c272671fea56d37246d57826ac78ec082f7fb2cbf3f906a8d0d2e974a26b8a28a9b05be5d2bae6b5406d6657611050fcdfd953657388af28e7b58cc06bcc2454f986dac75cbd3fe0a15518e9c37836cba4a130e8b96c42eae49e5436d0e1948ab4343abd54a6a72a46ff23afc84e8911469f1bdccab8a2becc7b32c4b96614977de91c406eef1fdbe8a573373141f73c383fa83af305bc20d660466e0e97c7f6f4f269e1676ed5b916f8f88c0ff2241c6ea05a5855007453c7186e5228c73591785f132dc04917180424b4b3d5670a1ee432c7989364530799c1b2960d339734821e3507725685913914e168cbce8c548382785d80c1d9a362caecf0b4d1c0681aae4864c151ee68148d993b7f14b14a11ccd0a261437e1ce836c19ac64258d27d7ca83c0a9311f40d8d47cb012b43a66605c39235ac62813e241b81f49138ecf5baabf6e32c3e330a4d0f034dd25f524042598f80c452878112220aec10cdcb320a9f118509ed454b641400e081a45baa4037888f5f0697cc3f4c614ec71b5d2a064bd7c7a32ba209a0c2080d0d86dba66e37d47113ae5e110c465fd4951c369f1c8d15eabe90f6f851ea66789e9d01f401275fcb1e91e38431d4714b58699f644b0074174b4d326278903db781af6d8f43eaa9127c66d18973c31bccbbc08eb040f8c0d3128bfbf888e1beb018b57318cce15d8ea00a78385cb6624c30118d590ae1a77b361ec8d9070e3d4c45bf1fae9b0fa13c447216a8aa08f5ea58ea7cc8042c79360f24a59b8a96b62b0786d776c8113ee09030247e22a7a15389e32030a1c4f829d11b826c6d3c463c58d6c2f0ba322b0bd8d85d1496aa4b10d2838d2f8776981ac2eca9b582cdc9a1f5b5bf1d7f4016555c519f42a751c590694386efc3ba3a90cdb15e84104a3b0e9362bd04580def62a8a180508375dc0822cd22a71ee699a69c224869f6b309506987030256c3a57078cf532ebaa8095e2b6366e51874f949124e515ee47d585783c2012949291a81d51220aa0372857bc8dd444c848310bd264e4a190c01d19009ddd86f6f5ed3f1902d9861b0034cf2b4810ea93280308e6628d04a3b1780c40e8b51b0902514936cdd9ab486a388c73d10050da5aaa602af69f0e60e93b465ab06497e10096bcf6a3855aa928035013202510664a3252aa78288ca9083f2726ea0aabb8a47a1cf5f492748e550c120387455c5c3bf8115b50437c5e40a6842eb086c31e08ad61306fe6b7860840300d4b00aa67fc8d9e6a38cdf055f11e6ac485888fd60410623c4ca2d461fce25309301974f10bca5100110c6d8902c42c58806b411ef95ebb4c18fdd1bbda3f2fbac119ec99f54b430df0ccbc9759a2be052893c27c20cc0d427b24cc0c445a4e3584d11e028b307ba00da4458c0794ba5140daa40361fa562cd20d66cdc451ed3ead8ecbda4f1b61d3d9876048af94696c0ca3e680cecfda591743e80cf1615178f4aa83209ba3a05663178e6f18182a1bdb1b15a8750d5341753a61733ed18a0ac2990240058ab4372a3093142604e035b7f49bb72287ec26b7d12c1d08413633301114be5f0bef6fabc1f31e5b401408c2de0800af0a3a97a48d53b2150d06580e5437a4e4d19b7c94b65e4a66044ad6d93a153d8903bdb6557bc1eab2934595a8887c385900198d4eae82cda630729a96e4cbeca64a6f74fedd8d7be29f750563c14b9ae8b3ab7bca933478404269b1e358a132e1c5cb200fee82e28ed9f96a2d55e37c7e80b7837625b9f564f65147086d52fccf3817d94c4f8d0b50768e92e6177864c5b6a91c2492bd0d8aa6b322cb541005a9e26ae479126dd731ece9855b33c76f2c10cda91c0cabbaffc782a9be3840a0618d1c1028d61186c306ebb2a07441bc1a1ad5576038128117636048cdcb9d2c24e8c9e112ce429018c9852e89a674bec10bbbd5548094a1d32c50bb706c2601d4b29f39c064cf6181309fed6135f9705850cd577b4824c10d37acea933d0c9abe860542bf398ca9ce60c38da9feea20ff728e1a6e22c8c5f6b049c61a161ef9e40883497a220163caeca1f2796958987c893d4421f90c0b522872c0924d31c321c916b4820750545dc361b59092ca70cb8654ea24f1627a1941f4c5e216b015388b650e6b949c81865bace4627bd8852e96755af37532ab16ecd9715ab794272f36cb16d0b09f55ab7e919c05517f7480c33e30cec1620b1ca485cda9c0090c5b309a2dd7c2de1d499aeb93956ee20c9c1d59c833d8f2ff4ba0fd99ff3e375bf2555816a4f9a2ec6862adf338bac8751d5ee22ed870d37e24fbb67a359db34eaa4f2ea633f3ee306f3433055fa522049d932ee2a23c1eb59015a05d3f82225b59aebbc6b66232266b9933550f3c6ee2bc5a325b034067218b9c6fe13f73f47e8dc433c5d15d37b6c9a174ee9cb380d1cf94f5c37ae6815ace62693e3bc2224fd04ac0c8f7490a137828db5698aa00ca6ec204c08077f7dccbaefce65efb1c2d0c937bae9573a0e89eab85e1b989ec0404033a9f6d2b1765f46a37b15083e85749d0307aced00742eb47e39b4f5e75e4931b8f86b2dcfae60b7f2cacde7bd51101361bacba32b48d02230f014f8e10442053c88a3f9a275098aeddb102efb55861a57f2608424c3ccc7766ac18e961712e07b29594db72551d19d2927cf0fd3c279eaa234d861334333fa5180db14aad26c897fa771da341e223b8c08d724c45184639968cc46a8801135595f98cda0fa7f39b272c1cebbda2c2decd6fd17914964e375ae12a88c37b2cd8d5fbbaf3c3fd83c3f9ec2c0a83ac8aa421a120c7e2cd2aabd89083a3223604add60bb1b97b84490125cb565c0a1b451e686590c510af578705558d4934ba26e48c3f07e9f263a0ca747d19afd0e3e9fc5f65d3e3d9e53f3fd4ad9fcddea698d7c7b3fdd9eff2fbda0d4e366930d85c1be598bba5dc6903424ed0dc068a9832b50d8c26bea382511c83e4e6fcadf669cd956fa30e20cd8d9cc1d262452126d44203b948e8e608b809bcf00b9744626880969930dd260d8dcca8a0e661fcd446da9ab80ceb21db20a788cde8862709ced092f0852b9662904605fd2e74478f0fcde0b1fcd33a78fcb32b6a42608607885c60865f785e48284760b487a508b98095a9cd60c5508bf6a829622bdaaf154d5485eb524e5b969f9fcd2eb3f771f8db1617dc626a082bba30b33a2d3eaa07e077d59212b2bc83b3a96de676cb955f21166cec4437c107ec220b4bb169c99b8ad33657bb08b7faddf06fd2ed59ba9546b68d2c9076bb248e3653548ede70a38bd8de813ed653431d43b1b373e3964d6c6f63d0b74d16adde3395e9a2dd846487d4a8eaadf25d15943e7693fec4c45faee95a647edf8d74d04ed2df59f4ed3c56169890667d68684348c4cecec069f15d4a2ddc690b2ea70f7600e7354570b7f9bf63a977bd3932afe5ccbade608f29f73ed3e90eb6b2a86323766d61a1a368814dd57448b6cb21173b750ed1cef3560c7a04cf9b26fcc28ee8f0c1b65c178e6b30336a30d368746797eb915d6bd67db3af07b7af7da4d2f59138b77e5e960535489edc415ef6869fce90fbd25c569952125cfad827cbb0fe13d44a8f39b6cb63d94e60a02bb4dee505bedda81097dd4865eb4f578cc0f8e1348513e727a228dcd2d1b6cb0d2b81a902f406130117bef81181728036dded6afad831f3c55a2f5e9ed3c30e626cd82f533b98fe75e034acd0e3b79dd27cb6cfa8397cf2ccaf2c71ab8319f295cad180c64c0b491ad5a0b1b9ca33b1858c7797a910b1c980b40bcb9af65ed604b592cd0dacc92c6ee38ad1604b9bbb0c8d6e21d5ee38bbecc89db33e0e6c180fe984b3b78bc7de5953a6dbe5371ed19851e48b627019389bf19029e374af7a4cccaaa1c26497bb7822c244023800611a2053f1d0c204bdea315161b2ce4bdc75417232b2774f049c2ce8a92c4b8ad73026ab48aa701d408ff498d8742435023c2e32512d62b7cf1e5fa0b8c006051a7d6e8e86b490356f644c5e8826bdbf1e538006db5d3b8bcfb80e9a96b9bca792be7bccc5cdf094d5345638e811b1c1f273cb297d449e92d789a842abe39d9a582d2914aa7ae5e674beba2b5e6aa8e2bd34c97ca53ee8ba2977414b943d649b3728af2689b10be22f927b2005ca0e80a48122f0fa945f825e97a8c083f95c45f8cdce42eaa02952f500a77813bb20ba5e824fbeab800319c4949099f32375174c05b02f3821a839afb85d5a71bd9401c913757d139f80b66f5247df3790b250d777a56ab55d5755f43dab53a2411d1b3a357568d799e0fe0727854978eda7076f11416acad49f5a550d9ccb9d4f8b4abf49eb912a0c966d587f14ad843113b5f35a9ecd14a21ba0a0beb9840c1e87e723137b5b1e0e33c4fe92addb20ae6a5705e4f532e89e52a82b53b4eae7a7add04f2c41ba9c8e543b4cb51da17a4bdefbb0a179eb96fbbceddc1d72e0504272639273e031522f02ae5cee15cfe2fa1f3ecd166b1ebefaddd38e223fdcc0c5037d1f39dddd0d8d1e15997078ed216bbbf3f0c03d099055c1dfa0fd2569ef34686e3304bcfeef6fd0be73b23b0fdd49e14d20e77a27deb2fb3ef50bfefe066c5c9bacdec9f6a1b014bb42f95d6fef03875725abc7b83b2e4a430dbae7bcf24a615509ea2022ef90475e7e8bfc64f16e1b17afc155bf5ea22c7c68409c6098315a72dee1bace657c9f5027b58011ad225c0ebe4279b00af2e02ccdc3fb6099e3e2e24dced2fc2c5f53295e86bd43abcbf8ed36df6c733c64b4be8b384741e1ecd6f57fb290703e795bbed994f9180246332c1ed07b1bffbc0da3558df785e2863200a2f0a29377760a5ee6c57b3b0f4f35a437496c098890af76fedfa2f526c2c0b2b7f14df019b5c10d8bdf6bf4102c9f9aa75720206646f0643f7919060f69b0ce088ca63dfe896578b57efcf17f3aa5d8ce43ea0000	6.2.0-61023	T
201809080702372_ChangeCommentReplyToAsOptional	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1d5d6fdcb8f1bd40ffc3629fda22e7f5c725b81af61d7c4edc1a17276eec1cfa16c8bbb423442bed49dac44671bfac0ffd49fd0ba52452e2d7f043a23e360dfce215c9e17066381c0e879cfffefb3f273f3daea3d967946661129fce0ff6f6e733142f9355183f9cceb7f9fd773fcc7ffaf18f7f3879b55a3fce7ea5f58e8a7ab8659c9dce3fe6f9e678b1c8961fd13ac8f6d6e1324db2e43edf5b26eb45b04a1687fbfb7f5d1c1c2c100631c7b066b39377db380fd7a8fc817f9e27f1126df26d105d252b1465e43b2eb929a1cede046b946d82253a9d87bfc4c997bdeb02932cc7a8a2f9ec2c0a038cc80d8aeee7b3208e933cc8319ac7ef337493a749fc70b3c11f82e8f6698370bdfb20ca1041ffb8a96e3b92fdc362248ba62105b5dc6679b2760478704448b3109bb722f0bc261d26de2b4ce4fca9187549c0d3f9d9320f3fe34ff399d8d9f179941615297dcf9314ed55ccd8a3ad9ecdcab267b518606929fe9ecdceb751be4dd1698cb6791a44cf66d7dbbb285cfe829e6e934f283e8db751c4628671c365dc07fce93a4d3628cd9fdea17b82efe56a3e5bf0ed1662c3ba19d3a61ac9659c1f1dce676f70e7c15d846ac633a3bec9f138ff86629406395a5d07798ed2b880814ad249bd0b7d9d6d3658c6d2a64b2c6e78e2cc6757c1e36b143fe41ff1943afc613ebb081fd18a7e2168bc8f433ccf70a33cdd22534f05f8ba132ce73553aa02698c0668c9265c9a09a507f28f2dca0a2276857316675f581ab683f21233f016ab140aa5f96d02f426f81c3e94e2a0e6ee7cf60e456579f631dc50f257651f081b42946126a7c9fa5d12352d99d20fb741fa80728c5d0256b949b6e952c0ef64d14c62fdd4a6c83acdecaad1ae4dec0eb3cc51aa2ec234cb8b7f355d3fdfefa3e7d7c1481de3e997269a5e0f9f3ff7a0cfb0ce5d15d25a75f3f353de8242c992ac935d2964d02ce83ec0d3e07299c46fb6ebbb8e58bf5a0761a425ef0b0f3897bd60abea3e4cd7a89e373f2758f504b133ced741967d49d2d5df83eca30675fcaf07d46fd0729b62dd769307eb4defbd5d7f4c62c4b37588bebcb1e6f64b72112cb115f32a2e5a75868727d5a7649bbf8a57c502fa3e5fcaeba925002fe89c2d9728cb2eb030a3d57982370ddd8c8462b1332855dbf9d7deaa602c061f8605b51a348605b53dac512c2d32037eb48e02b9aa08c68c94bba2751e05e15a8f15ad222355958038916257942e92284abe601952a355dad9759d0f9581d6e0a62896ec44551d95a1a8c3f275f210c67ac2d12a32e1aa129070a4d8957074f3a0c78aa92523561782b835355cd12be0e851233564b4ca0210a5aad4159d5208f4f8d02a3242550988112956a164bff92867b4e3dea36cb36b5b8ffe7d0ad834c851b3d075b0461c17c7f3141508170bbdfb265a5c68372b18d6406e058fbe997e5d08e69514741e082badf54a9aacd758c6d41a85147ea093ba418b2f91b0128a5d91a28c5722450b55d412cb24c4a40a9d3c2d64986eda8e34faa6eebe4a75e7c785399c37f91dda4458ae88504a78bbed13ba2911d12c01748c0f855b83a675144811c50a62458d26577b9223b8163bb1aa8c245f03c455a8d6c9c06b74b38bd2a3adbe693dc96d13e691de0b61e764753d2441d9320d3706afe9c1febe0fbfe9340c308d2bc3c9a61067196874f8340d2df7bf907928ef903d6c38eb81cb3b4ea108a6998f3d6709c34d1f954dbe2923b1af518e99fa5545a659d7d197024d3841eefbf12212c107dd88e54f931fb1aad4768bd65933803b340d05dd34433dd4162aa26e3bbcaea02be6c25cb50ee270d52d83adca5ea24ef46b94bf09232e56da59658b23b8c2b73819302008ee4bace70d5d2f0a48e579886eea5cd5516867d9e60dcaf768ebbd0aee458a617e49d24f7b12d86733ebc6cdcc3bb49d79470777f7473f3c7f11ac8e5e7c8f8e9e7f8d2bf65833b8641f178ad6d75979d9d3af41b4f5dd55abd9501e72f99f0d25d8e9cf86124dfcf9735886ec582c4db432066f555fbdea99e79c80d9d0d3811be6d09d0fa3035a4d97627df23f5b0aa8d39f2c0e065c31a05db2df28be139338ffd2b61b92b62b31b463075cc1fb7d96df1f48b5c606974b25135c51c5cd02dfae15b7332a03eb32bb888287e6ba4a9b1b1b4579c73d2c66035ee1a227cc36d6bae5697f858aa046ce9b41365da50987454b6217d7a4f2a136870ca4957c14c5b75aada42647fa26afc34f881e499116dfcbbcaab8c27e3ccbb264199674e71d496c9c1edfefab78353307ed31173aa83ff80ab325dc604660069ecef7f7f664e26961d7be15f9b28808fc2f1264ac5e505a087550840f67582ec238977551182fc34d10190728b4b4d4630517ea3ec492976853c48ec7b99102369d338714320e755782963511e964c1c88b5e8c84735288cdd0a169c3e2fabcd0c46110a84a6ec854e1611e88943d791bbf4411cad1ac604271f1ed3cc896c14a56d27872ad3c089c1af301844dcd072b41ab4304c69533a2658c32211e84fb9134e1f8bca5faeb26333c0e430a0d4fd35d524f42508289cf508482172102e21accc0fdcb921a9501454a4d681b04e410a051044c3a8387b80f1fc8377c6facc11e172c0d4ad62ba827bb0ba2c90002088ddda66b36c27714a1539e0f415cd61f16359c164f1ded159bfefc56e8617ac6980efd012451c71f9bee8163d47144516b9b690f057b104447536d729238b099a7618f4defa3da7962e88671c91323bccc8bb04ef0c0f01083f2fb8be8bbb11eb078f9c2e85f81ad0e703a58786d46321c80510de9ad71371bc6de0b09774c4dbc152f9c0eab3f417c14a2a688fbe855ea78ca0c28743c0926af9485bbb926068b1775c71638e16630207024b4a25781e32933a0c0f124d819816bc23c4d3c56dcc1f6b2302a62dbdb58189da4461adb8082238d7f9716c8ea6abc89c5c23df9b1b5157f311f505655a841af52c791654089e3c6bf339acab05d819e40300a9b6eb302dd05e86dafa208538070d3c52cc822ad12e79ea699265262f8b906536980090753c2a67375cc582fb3ae8a5929ee67e316750445194c525eda7e545d81c7032271291909dc1125a2007a8372c56b484d908c14b6204d461e0a89dd9101d0d96d685f5f009421906db80140f3a08204a13e8c328060eed648301a8bc70084debc9120109564d39cbd8da486c338170d00a5ada50aa662ffe90096be5ca4054b76190e60c9fb3e5aa8958a32003501520261a62423a58aa7c1988af00362a2aeb00a4daac7514f2f49e7588521317058c4c5b5831fb10535c41706644ae8626b38ec81e81a06f3667e6b8800c4d3b004a07ac6dfe8a986d30c5f15f2a1465c08fa684d0021ccc3244a1dc62fbe9600934117c2a01c0510c4d0962840d88205b816e491afb6cb84d11fbdabfdf3a21b9cc19e59bf34d400cfcc7b9925ea8b803229cc07c2dc20b447c2cc40a4e5544318ed21b008b307da405ac47840a91b05a44d3a10a66fc5225d62d64c1cd5eed3eab8acfdb411369d7d0886f42e99c6c6306a0ee8fcac9d753184ce109f128547af3a08b2390a6a3576e1f88681a1b2b1bd51815ad7301554a71336e713ada8209c290054a0487ba30233496142005e734bbf792b72c86e721bcdd281106433031341e1fbb5f0feb61a3cefb105448120ec8d00f0aaa07349da38255bd16080e54075494a1ebdc94769eba56446a0649dad53d19338d09b5bb517ac2e3b5954a989c887930590c3e8e42ad86c0a23a76949becc6eaa8446e7dfddb8a7fa59573016bca4893ebbbaa73c4983072494163b8e152a535cbc0cf2e02e28ae999dafd65235cee707783b6857925b4f661f7584d026c5ff8c7391cdedd4b80065e728697e8147566c9bca4122d9dba0683a2bf24a0551902a6e479e27d1761dc39e5eb83573fcc602d19ccac1b0aa2b802c98ea8b03041ad6c80181621d61386cb02e0b4a17c4aba1517d0b86231178370686d4bcd5c942821e192ee12c0489915ce892684ae71bbcb05b4d0548193acd02b50bc76612402dfb99034cbe1c1608f3d91e5693018705d57cb5874452da70c3aa3ed9c3a0096b5820f49bc398ea9c35dc98eaaf0ef22f67a5e126825c6c0f9be4a861e1914f8e309834271230a6cc1e2a9f898685c997d84314d2cdb0208522072cd9a4321c926c412b780045d5351c560b298d0cb76c48a54e122f269411445f2c6e015b81b358e6b046c93967b8c54a2eb6875de86259a7355f27b36ac19e1da7754b79f262b36c010dfb59b5ea37c85910f5470738ec93e21c2cb6c0415ad82c0a9cc0b005a3d9722decdd91a4b93e59e926cec0d991853c832dffbf04da9ff9ef73b3255f8565419a2fca8e26d63a8fa38b5cd7e125ee820d37ed47b26fab87d339eba4fae4623a334f0ff3463353f0552a42d039e9222ecae3510b5901daf52328b295e5ba6b6c2b2663b2963953f5c0e326ceab25b335007416b2c8f916fe3347efd7483c531cdd75639b1c4ae7ce390b18fd4c593fac67dea8e52c96e6b3232cf20aad048c7c9fa4308187b26d85a90aa0ec264c000c7877cf3deeca6feeb52fd2c230b9175b39078aeec55a189e9bc84e4030a0f3d9b6725146af76130b35887e95040da3e70c7d20b47e34bef9e455473eb9f16828cbad6fbef0c7c2eabd571d1160b3c1aa2b43db2830f210f0e40841043285acf8a3790285e9da1d2bf05e8b1556fa678220c4c4c37c67c68a911e16e772205b49b92d57d591212dc907dfcf73e2a93ad264384133f3538ad110abd46a827ca97fd7311a243e820bdc28c75484619463c948ac861830515599cfa8fd703abf79c2c2b1de2b2aecddfc169d4761e974a315ae8238bcc7825d3db13b3fdc3f389ccfcea230c8aa481a120a722cdeacb28a0d39382a6243d06abd109bbb47981450b26cc565b151647e5606590cf180755850d59847a36b0acef873902e3f06aadcd697f10a3d9eceff55363d9e5dfef343ddfad9ec6d8a797d3cdb9ffd2e3fb1dde0649309834db7518eb95bd69d3620e494cc6da0884952dbc068e23b2a18c531486eced86a9fc85cf93cea00d2dcc8192c2d561462422d34908b9c6e8e809bc00bbf704924860668990cd36dd2d0c88c0a6a1ec64f6da4ad89cbb01eb20d728ad88c6e7892e00c2d095fb86229066954d0ef4277f4f8d00c1ecb3fad83c73fbba22604667880c80566f885e785847204467b588a900b5899da0c560cb5688f9a22b6a2fd5ad14455b82ee5b465f9f9d9ec327b1f87bf6d71c12da686b0a20b33abd3e2a37a037e572d2921af3b389bdae66ab75cf91562c1c64e74137cc02eb2b0149b96bca9386d73b58b70ab9f0eff26dd9ea55b6964dbc80269b74be2683345e5e80d37ba88ed21fa744834a80ea1d8d9a971cba6b6b7b1e7dba68b566f99ca84d16e32b2435a54f554f9ae0a4a1f9b497f62e22fdb742d32bfef46426827e9ef2cfa760e2b0b4c48338705ac4362e4af63064e8bef5272e14e3b703981b00338af4982bbcdff1d4bbeebcd8f792de7d6f5067b4cb9f7995077b095451d1ab16b0b0b1d450b6caaa643b25d8eb8d8a96388768eb762d02338de34d117764487cfb5e5ba7058839951839946a3fbba5c4fec5ab3ee9b7d3db87ded2399ae8fd4b9f5ebb22ca84132e50ef2b037fc7286dc97e6aeca94d2e0d2b73e5986f59fa2567acbb15d26cb760203dda0f52e2ff0e54685b8ec46325b7fba6204c60fa7299c383f1145e19690b6757658095215a2379814b8b0c68f149403b4e96e5713c88e9931d67afdf29c2076107bc37ea5dac104b0032762859ebfed94e8b37d4ecde1d3677e65a95b1d2c91af548e06b4675a48d2a8368dcd659e892d64bcc74c85884d0ea45d58d6b437b326a8956cee604d66711b578c065bdadc6568740ba9f6c8d9e547ee9cf77160c378483f9cbd5d3cf6e69a32dd2ec3f188c68c22631483cbc0f98c874c1aa77bd76362560d1526bbecc513112612c30108d300b98a871626e85d8f890a937566e2ae0b929391bd7b22e064414f655952bc873159455245ec007aa4c7d4a623a911e07991896a11bb7df6f802c5c53628d0e8737334a485ac792563f24234e9fdf5980234d8eeda597cc675d0b4cce63d9504de632e6e86c7aca6b1c241cf880d96a15b4eea23f294bc4f44155a1df2d4846b49d150d53b37a7f3d55df1564315f2a549e72bf541d74db90b5aa2ec21dbbc417935498c5d107f91dc0329507600a40d1481d7a7fc12f4ba44051ecce82ac26f761652074d91aa0738c99bd805d1f5127cf25d051cc821a684cc9c1fa9bb602a807dc12941cd99c5ed128beba50c489fa8eb9bf804b47d933afabe81a485babe2b55abedbaaaa2ef599d140dead8d0a9a943bbce04f73f38294cc26b3f3d788b085253a6fed4aa6ae06cee7c6254fa4d5a8f5491b06cc3faa368258c99aa9dd7f26cae10dd0005f5cda564f0383c1fb9d8dbf2709821f6976edd067155bb2a20af9741f794445d99a4553f3f6d857e6229d2e584a4da61aaed08d56bf2de870dcd5bb7ece76de7ee90038752921bd39c03cf917a1170e572af7818d7fff069be58f3f0d52f9f7614f9e1062e1ee8fbc8eaee6e68f4a8c884c36b0f79db9d8707ee4980bc0afe06ed2f4d7ba741739b21e0fd7f7f83f69d95dd79e84e0a6f0259d73bf196ddf7a9dff0f73760e3da64f552b60f85a5d815ca2f7b7b1f38bc2a593dc7dd71511a6ad03d6796570aab4a50071179874cf2f26be4278b77dbb8780faefaf51265e14303e204c38cd192f30ed7752ee3fb843aa9058c6815e17ef015ca8355900767691ede07cb1c1717af7296e667f9a04af136ec1d5a5dc66fb7f9669be321a3f55dc4390a0a67b7aeff938584f3c9dbf2d9a6ccc710309a61f184dedbf8e76d18ad6abc2f149794011085179d3cb553f0322f9edc7978aa21bd49624b40847cb5f3ff16ad37110696bd8d6f82cfa80d6e58fc5ea38760f9d4bcbe020131338227fbc9cb3078488375466034edf14f2cc3abf5e38fff0328f550a937ea0000	6.2.0-61023	T
201809110518398_FixImageLinksInAnswers	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1d5d6fdcb8f1bd40ffc3629fda22e7f5c725b81af61d7c4edc1a17276eec1cfa16c8bbb423442bed49dac44671bfac0ffd49fd0ba52452e2d7f043a23e360dfce215c9e17066381c0e879cfffefb3f273f3daea3d967946661129fce0ff6f6e733142f9355183f9cceb7f9fd773fcc7ffaf18f7f3879b55a3fce7ea5f58e8a7ab8659c9dce3fe6f9e678b1c8961fd13ac8f6d6e1324db2e43edf5b26eb45b04a1687fbfb7f5d1c1c2c100631c7b066b39377db380fd7a8fc817f9e27f1126df26d105d252b1465e43b2eb929a1cede046b946d82253a9d87bfc4c997bdeb02932cc7a8a2f9ec2c0a038cc80d8aeee7b3208e933cc8319ac7ef337493a749fc70b3c11f82e8f6698370bdfb20ca1041ffb8a96e3b92fdc362248ba62105b5dc6679b2760478704448b3109bb722f0bc261d26de2b4ce4fca9187549c0d3f9d9320f3fe34ff399d8d9f179941615297dcf9314ed55ccd8a3ad9ecdcab267b518606929fe9ecdceb751be4dd1698cb6791a44cf66d7dbbb285cfe829e6e934f283e8db751c4628671c365dc07fce93a4d3628cd9fdea17b82efe56a3e5bf0ed1662c3ba19d3a61ac9659c1f1dce676f70e7c15d846ac633a3bec9f138ff86629406395a5d07798ed2b880814ad249bd0b7d9d6d3658c6d2a64b2c6e78e2cc6757c1e36b143fe41ff1943afc613ebb081fd18a7e2168bc8f433ccf70a33cdd22534f05f8ba132ce73553aa02698c0668c9265c9a09a507f28f2dca0a2276857316675f581ab683f21233f016ab140aa5f96d02f426f81c3e94e2a0e6ee7cf60e456579f631dc50f257651f081b42946126a7c9fa5d12352d99d20fb741fa80728c5d0256b949b6e952c0ef64d14c62fdd4a6c83acdecaad1ae4dec0eb3cc51aa2ec234cb8b7f355d3fdfefa3e7d7c1481de3e997269a5e0f9f3ff7a0cfb0ce5d15d25a75f3f353de8242c992ac935d2964d02ce83ec0d3e07299c46fb6ebbb8e58bf5a0761a425ef0b0f3897bd60abea3e4cd7a89e373f2758f504b133ced741967d49d2d5df83eca30675fcaf07d46fd0729b62dd769307eb4defbd5d7f4c62c4b37588bebcb1e6f64b72112cb115f32a2e5a75868727d5a7649bbf8a57c502fa3e5fcaeba925002fe89c2d9728cb2eb030a3d57982370ddd8c8462b1332855dbf9d7deaa602c061f8605b51a348605b53dac512c2d32037eb48e02b9aa08c68c94bba2751e05e15a8f15ad222355958038916257942e92284abe601952a355dad9759d0f9581d6e0a62896ec44551d95a1a8c3f275f210c67ac2d12a32e1aa129070a4d8957074f3a0c78aa92523561782b835355cd12be0e851233564b4ca0210a5aad4159d5208f4f8d02a3242550988112956a164bff92867b4e3dea36cb36b5b8ffe7d0ad834c851b3d075b0461c17c7f3141508170bbdfb265a5c68372b18d6406e058fbe997e5d08e69514741e082badf54a9aacd758c6d41a85147ea093ba418b2f91b0128a5d91a28c5722450b55d412cb24c4a40a9d3c2d64986eda8e34faa6eebe4a75e7c785399c37f91dda4458ae88504a78bbed13ba2911d12c01748c0f855b83a675144811c50a62458d26577b9223b8163bb1aa8c245f03c455a8d6c9c06b74b38bd2a3adbe693dc96d13e691de0b61e764753d2441d9320d3706afe9c1febe0fbfe9340c308d2bc3c9a61067196874f8340d2df7bf907928ef903d6c38eb81cb3b4ea108a6998f3d6709c34d1f954dbe2923b1af518e99fa5545a659d7d197024d3841eefbf12212c107dd88e54f931fb1aad4768bd65933803b340d05dd34433dd4162aa26e3bbcaea02be6c25cb50ee270d52d83adca5ea24ef46b94bf09232e56da59658b23b8c2b73819302008ee4bace70d5d2f0a48e579886eea5cd5516867d9e60dcaf768ebbd0aee458a617e49d24f7b12d86733ebc6cdcc3bb49d79470777f7473f3c7f11ac8e5e7c8f8e9e7f8d2bf65833b8641f178ad6d75979d9d3af41b4f5dd55abd9501e72f99f0d25d8e9cf86124dfcf9735886ec582c4db432066f555fbdea99e79c80d9d0d3811be6d09d0fa3035a4d97627df23f5b0aa8d39f2c0e065c31a05db2df28be139338ffd2b61b92b62b31b463075cc1fb7d96df1f48b5c606974b25135c51c5cd02dfae15b7332a03eb32bb888287e6ba4a9b1b1b4579c73d2c66035ee1a227cc36d6bae5697f858aa046ce9b41365da50987454b6217d7a4f2a136870ca4957c14c5b75aada42647fa26afc34f881e499116dfcbbcaab8c27e3ccbb264199674e71d496c9c1edfefab78353307ed31173aa83ff80ab325dc604660069ecef7f7f664e26961d7be15f9b28808fc2f1264ac5e505a087550840f67582ec238977551182fc34d10190728b4b4d4630517ea3ec492976853c48ec7b99102369d338714320e755782963511e964c1c88b5e8c84735288cdd0a169c3e2fabcd0c46110a84a6ec854e1611e88943d791bbf4411cad1ac604271f1ed3cc896c14a56d27872ad3c089c1af301844dcd072b41ab4304c69533a2658c32211e84fb9134e1f8bca5faeb26333c0e430a0d4fd35d524f42508289cf508482172102e21accc0fdcb921a9501454a4d681b04e410a051044c3a8387b80f1fc8377c6facc11e172c0d4ad62ba827bb0ba2c90002088ddda66b36c27714a1539e0f415cd61f16359c164f1ded159bfefc56e8617ac6980efd012451c71f9bee8163d47144516b9b690f057b104447536d729238b099a7618f4defa3da7962e88671c91323bccc8bb04ef0c0f01083f2fb8be8bbb11eb078f9c2e85f81ad0e703a58786d46321c80510de9ad71371bc6de0b09774c4dbc152f9c0eab3f417c14a2a688fbe855ea78ca0c28743c0926af9485bbb926068b1775c71638e16630207024b4a25781e32933a0c0f124d819816bc23c4d3c56dcc1f6b2302a62dbdb58189da4461adb8082238d7f9716c8ea6abc89c5c23df9b1b5157f311f505655a841af52c791654089e3c6bf339acab05d819e40300a9b6eb302dd05e86dafa208538070d3c52cc822ad12e79ea699265262f8b906536980090753c2a67375cc582fb3ae8a5929ee67e316750445194c525eda7e545d81c7032271291909dc1125a2007a8372c56b484d908c14b6204d461e0a89dd9101d0d96d685f5f009421906db80140f3a08204a13e8c328060eed648301a8bc70084debc9120109564d39cbd8da486c338170d00a5ada50aa662ffe90096be5ca4054b76190e60c9fb3e5aa8958a32003501520261a62423a58aa7c1988af00362a2aeb00a4daac7514f2f49e7588521317058c4c5b5831fb10535c41706644ae8626b38ec81e81a06f3667e6b8800c4d3b004a07ac6dfe8a986d30c5f15f2a1465c08fa684d0021ccc3244a1dc62fbe9600934117c2a01c0510c4d0962840d88205b816e491afb6cb84d11fbdabfdf3a21b9cc19e59bf34d400cfcc7b9925ea8b803229cc07c2dc20b447c2cc40a4e5544318ed21b008b307da405ac47840a91b05a44d3a10a66fc5225d62d64c1cd5eed3eab8acfdb411369d7d0886f42e99c6c6306a0ee8fcac9d753184ce109f128547af3a08b2390a6a3576e1f88681a1b2b1bd51815ad7301554a71336e713ada8209c290054a0487ba30233496142005e734bbf792b72c86e721bcdd281106433031341e1fbb5f0feb61a3cefb105448120ec8d00f0aaa07349da38255bd16080e54075494a1ebdc94769eba56446a0649dad53d19338d09b5bb517ac2e3b5954a989c887930590c3e8e42ad86c0a23a76949becc6eaa8446e7dfddb8a7fa59573016bca4893ebbbaa73c4983072494163b8e152a535cbc0cf2e02e28ae999dafd65235cee707783b6857925b4f661f7584d026c5ff8c7391cdedd4b80065e728697e8147566c9bca4122d9dba0683a2bf24a0551902a6e479e27d1761dc39e5eb83573fcc602d19ccac1b0aa2b802c98ea8b03041ad6c80181621d61386cb02e0b4a17c4aba1517d0b86231178370686d4bcd5c942821e192ee12c0489915ce892684ae71bbcb05b4d0548193acd02b50bc76612402dfb99034cbe1c1608f3d91e5693018705d57cb5874452da70c3aa3ed9c3a0096b5820f49bc398ea9c35dc98eaaf0ef22f67a5e126825c6c0f9be4a861e1914f8e309834271230a6cc1e2a9f898685c997d84314d2cdb0208522072cd9a4321c926c412b780045d5351c560b298d0cb76c48a54e122f269411445f2c6e015b81b358e6b046c93967b8c54a2eb6875de86259a7355f27b36ac19e1da7754b79f262b36c010dfb59b5ea37c85910f5470738ec93e21c2cb6c0415ad82c0a9cc0b005a3d9722decdd91a4b93e59e926cec0d991853c832dffbf04da9ff9ef73b3255f8565419a2fca8e26d63a8fa38b5cd7e125ee820d37ed47b26fab87d339eba4fae4623a334f0ff3463353f0552a42d039e9222ecae3510b5901daf52328b295e5ba6b6c2b2663b2963953f5c0e326ceab25b335007416b2c8f916fe3347efd7483c531cdd75639b1c4ae7ce390b18fd4c593fac67dea8e52c96e6b3232cf20aad048c7c9fa4308187b26d85a90aa0ec264c000c7877cf3deeca6feeb52fd2c230b9175b39078aeec55a189e9bc84e4030a0f3d9b6725146af76130b35887e95040da3e70c7d20b47e34bef9e455473eb9f16828cbad6fbef0c7c2eabd571d1160b3c1aa2b43db2830f210f0e40841043285acf8a3790285e9da1d2bf05e8b1556fa678220c4c4c37c67c68a911e16e772205b49b92d57d591212dc907dfcf73e2a93ad264384133f3538ad110abd46a827ca97fd7311a243e820bdc28c75484619463c948ac861830515599cfa8fd703abf79c2c2b1de2b2aecddfc169d4761e974a315ae8238bcc7825d3db13b3fdc3f389ccfcea230c8aa481a120a722cdeacb28a0d39382a6243d06abd109bbb47981450b26cc565b151647e5606590cf180755850d59847a36b0acef873902e3f06aadcd697f10a3d9eceff55363d9e5dfef343ddfad9ec6d8a797d3cdb9ffd2e3fb1dde0649309834db7518eb95bd69d3620e494cc6da0884952dbc068e23b2a18c531486eced86a9fc85cf93cea00d2dcc8192c2d561462422d34908b9c6e8e809bc00bbf704924860668990cd36dd2d0c88c0a6a1ec64f6da4ad89cbb01eb20d728ad88c6e7892e00c2d095fb86229066954d0ef4277f4f8d00c1ecb3fad83c73fbba22604667880c80566f885e785847204467b588a900b5899da0c560cb5688f9a22b6a2fd5ad14455b82ee5b465f9f9d9ec327b1f87bf6d71c12da686b0a20b33abd3e2a37a037e572d2921af3b389bdae66ab75cf91562c1c64e74137cc02eb2b0149b96bca9386d73b58b70ab9f0eff26dd9ea55b6964dbc80269b74be2683345e5e80d37ba88ed21fa744834a80ea1d8d9a971cba6b6b7b1e7dba68b566f99ca84d16e32b2435a54f554f9ae0a4a1f9b497f62e22fdb742d32bfef46426827e9ef2cfa760e2b0b4c48338705ac4362e4af63064e8bef5272e14e3b703981b00338af4982bbcdff1d4bbeebcd8f792de7d6f5067b4cb9f7995077b095451d1ab16b0b0b1d450b6caaa643b25d8eb8d8a96388768eb762d02338de34d117764487cfb5e5ba7058839951839946a3fbba5c4fec5ab3ee9b7d3db87ded2399ae8fd4b9f5ebb22ca84132e50ef2b037fc7286dc97e6aeca94d2e0d2b73e5986f59fa2567acbb15d26cb760203dda0f52e2ff0e54685b8ec46325b7fba6204c60fa7299c383f1145e19690b6757658095215a2379814b8b0c68f149403b4e96e5713c88e9931d67afdf29c2076107bc37ea5dac104b0032762859ebfed94e8b37d4ecde1d3677e65a95b1d2c91af548e06b4675a48d2a8368dcd659e892d64bcc74c85884d0ea45d58d6b437b326a8956cee604d66711b578c065bdadc6568740ba9f6c8d9e547ee9cf77160c378483f9cbd5d3cf6e69a32dd2ec3f188c68c22631483cbc0f98c874c1aa77bd76362560d1526bbecc513112612c30108d300b98a871626e85d8f890a937566e2ae0b929391bd7b22e064414f655952bc873159455245ec007aa4c7d4a623a911e07991896a11bb7df6f802c5c53628d0e8737334a485ac792563f24234e9fdf5980234d8eeda597cc675d0b4cce63d9504de632e6e86c7aca6b1c241cf880d96a15b4eea23f294bc4f44155a1df2d4846b49d150d53b37a7f3d55df1564315f2a549e72bf541d74db90b5aa2ec21dbbc417935498c5d107f91dc0329507600a40d1481d7a7fc12f4ba44051ecce82ac26f761652074d91aa0738c99bd805d1f5127cf25d051cc821a684cc9c1fa9bb602a807dc12941cd99c5ed128beba50c489fa8eb9bf804b47d933afabe81a485babe2b55abedbaaaa2ef599d140dead8d0a9a943bbce04f73f38294cc26b3f3d788b085253a6fed4aa6ae06cee7c6254fa4d5a8f5491b06cc3faa368258c99aa9dd7f26cae10dd0005f5cda564f0383c1fb9d8dbf2709821f6976edd067155bb2a20af9741f794445d99a4553f3f6d857e6229d2e584a4da61aaed08d56bf2de870dcd5bb7ece76de7ee90038752921bd39c03cf917a1170e572af7818d7fff069be58f3f0d52f9f7614f9e1062e1ee8fbc8eaee6e68f4a8c884c36b0f79db9d8707ee4980bc0afe06ed2f4d7ba741739b21e0fd7f7f83f69d95dd79e84e0a6f0259d73bf196ddf7a9dff0f73760e3da64f552b60f85a5d815ca2f7b7b1f38bc2a593dc7dd71511a6ad03d6796570aab4a50071179874cf2f26be4278b77dbb8780faefaf51265e14303e204c38cd192f30ed7752ee3fb843aa9058c6815e17ef015ca8355900767691ede07cb1c1717af7296e667f9a04af136ec1d5a5dc66fb7f9669be321a3f55dc4390a0a67b7aeff938584f3c9dbf2d9a6ccc710309a61f184dedbf8e76d18ad6abc2f149794011085179d3cb553f0322f9edc7978aa21bd49624b40847cb5f3ff16ad37110696bd8d6f82cfa80d6e58fc5ea38760f9d4bcbe020131338227fbc9cb3078488375466034edf14f2cc3abf5e38fff0328f550a937ea0000	6.2.0-61023	T
201809110911568_AddedAnswerLikesTable	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1d5d6fdcb8f1bd40ffc3629fda22e7f5c725b806f61d7c4edc1a97afc6c9a16f81bc4b3b42b4d29ea44d6c14f7cbfad09fd4bf504aa2247eccf043a2a4dd34f08b57248733c3e170c81972fefbefff9cfe74bf8e669f499a85497c363f3a389ccf48bc4c56617c7736dfe6b7dffd30ffe9c73ffee1f4f96a7d3ffbb5ae7752d4a32de3ec6cfe31cf374f178b6cf991ac83ec601d2ed3244b6ef38365b25e04ab64717c78f8d7c5d1d1825010730a6b363b7dbb8df3704dca1ff4e745122fc926df06d1cb6445a28c7da725d725d4d9ab604db24db02467f3f09738f972f0a6c024cb29aa643e3b8fc28022724da2dbf92c88e3240f728ae6d3f719b9ced324bebbded00f41f4ee614368bddb20ca0843ff695bdd9692c3e3829245dbb006b5dc6679b276047874c258b3909b7762f0bc611d65de73cae4fca1a0ba64e0d9fc7c99879fe9a7f94ceeece9459416156bfe5e242939a806e3a06ef56856963d6ac4804a4bf1f76876b18df26d4ace62b2cdd3207a347bb3bd89c2e52fe4e15df289c467f1368a78cc286eb44cf8403fbd49930d49f387b7e496e17bb59acf1662bb85dcb069c6b5a928b98af393e3f9ec15ed3cb8894833f01cd5d739a5f36f2426699093d59b20cf491a173048c93aa577a9aff3cd86ca58da7649c58d4e9cf9ec6570ff82c477f9473aa58e7f98cf2ec37bb2aabf3034dec7219d67b4519e6e89a9a7027cd30995f36650aa02854603b464132ecd8cd203f9c796640513fbc2398fb32f3c0fbb41794607f01d55293594f6b709d0abe07378578a033cbaf3d95b1295e5d9c77053b3bf2afbc0862124191de43459bf4da2b62557fae15d90de919c6297a055ae936dba94f03b5db493583fb56b649d6676d568df26768f59e6285597619ae5c5bf9aae1f1f0ed1f38b60a28ee9f44b134dafc78f1f7bd06754e7ae0a69adbaf9f921efc0a164c9d6c9be1c326816721bd06970b54ce257dbf54d4fac9faf8330d2b2f789079ccb5ea855751ba66bd2cc9b9f13aa7a82d819e73741967d49d2d5df83eca30675faaf07d4afc9729b52dd769d07ebcde0bdbdf998c4441cd631faf23634efbe2497c1925a31cfe3a2556f7874527d4ab6f9f378552ca0eff3a5ba9e5a02f082cef97249b2ec920a33595d2474d3d0cf4828163b8352b59d7fddad0ace62f06158d45683c6b0a86d0f6b144b8bcc805f5d0740ae2ac23163e5ae685d4441b8d66355575191aa4a509c58b12b4a974914255fa80cc16895767653e74365a0b5b801c58a9d08d5810c451d962f92bb30d633aeaea232ae2a4119c78a5d19576f1ef45871b554c49a4214b7b6862b7a051c3d6aac868a565980a25495baa2530a811e9fba8a8a50558262c48a2194ec371fe58c76dc7b946df66deb31fc9902350d72d22e743dac11c7c5f1222505c2c542efbe899617dacd0a8735d2b182c7b399618f10cc2b297a7820adb4d62b69b25e531983350a2bfc504fea162db144c14a2a7645aa1e7810a9ba10e2965ca620a654e875d2c2c874d376acd13775f755aa3b3f4798e39d26bf259b88ca15134a056fb77d423f25229b25888ef1a1701bd0751d0029a65851ac6aa3c9d59e1418aec54eaeaa2229d6407195aaf532f05addeca2f4ea56dfb49e726c13e691fe14c2ee90d5d54942b2651a6e0ca7a64787873ece4d77c300d31c6538d914f22c438d0e9fa6a1e5fe17330fd51db2870d6743b8bae3948a709ef9d8739630dcf451d9e49b3292fb9ac4cd34ac2a32cdba9e6729d88493e47e98534426f8e83162f9d3748e5855eaba45ebad19d01d9a86836e9aa121b5838a68da8eaf2bea157361aeda0471b8ea96d156652f5127fa35cadf8491172bedacb2c5115de13b78060c08a2fb12eb7953af1705a4d21fa29b3a2f9b28b4f36cf38ae40775eb830aee654a617e49d24f070ad84733ebc6edcc3bb69d79274737b7273f3c7e12ac4e9e7c4f4e1e7f8d2bf65433b81c3e21146d285f79d9d3af41b4f5dd55a7d9503ab9fccf8612eceecf86124dfaf9735886ec582c4d75650adeaa3ebcea99e79c84d9d8d3412073eccec7d1019da64bb13ef99f2d05d4dd9f2c0e065c41d03ed96f35be3b2271d5c1cb8bf09356d630c773d1eedb51c0742e888e2e932ebe8876c001778452a8064a2835bc1ef571d055bf845aaa454fe39d7056e4fe95f87e28f07d094d9f3a8e113f46e3c7fb03abd68ab45aaa883450c54da4b76be0d253b56fb9ca2ea3e0aebd05d6e5225451de73eda0c3400dc7e8810e1bafff44debf2445acb07048c8ce32ca9d11152d65b88426956a687d77ac95eae1155bad564a93137d9342fbd4aa95b5f85e1dab6a54f88fe759962cc392efa2aae4c35fc57e9fc7ab99391696bb27552bd5977458c20d1d083a8067f3c3830395795ad8cd91a57a074b06fe170532552f242d843a28a2f2332a17619cabba288c97e126888c044a2d2df558310a4d1f72c933b229ae64c4b99103369d7376848a43d395a4654d4c3a5d70f2a2172329fc001b662c16a11de2c60d6f1a61142824376caa88308f64ce9ebe8e9f9188e464560c42719ff422c896c14a55d27472ad3c081c8cf908c2068f8395a03566e4b472c6b48c51266423ce8fa449765f47f5d74f66441cc6141a91a7fba49ea4581fd33863813f5e840809173203f72f4b302a238a14cc681b04d4c8ba49044c096dc1461f8f7369c7bdb506075cb0342859afa09eec2e8c2723082046bb4dd77ce0fc244207ba5db151d6fb60db91969df9f68a4d1f1621f5b07bc6980efd112451373e36dd23d109d388a2d636d3fada07104447536de7247164334f333c36bd4f6ae7c91151c6254f0e9c342fc23ac143a3ae0ccaef2ff2d98d35c1f29d26e3f90a6e75a0d3c1e2d46622c301a16accd31a77b361eabd907475db34b6f23dee71f5278a0f206a4038d5a05227726644a11359b0f34a59baf26e1a60f9fefbd402275db847048e452c0d2a70226746143891057b23706df4b4698c81a70dbc2c8cc095912e16462fa951681b517014faf76981ac5e9c300db1f4fcc4d4da4a7cef02515655a8c1a05227b065448913e8df1b4d65d8ae602f8b18854db759c1aed80cb75751c28250d4f0182179675185a339508d0617ed81bf10457e8c09868d89954a9fd66b08c47cd9c807ee3bec2f7b137a1071344616a3fdf323029156d860ebc2aed455195a9107b21434c15ee39b0b38974690459c13369dc3d1e4830862157657bcdc425b344160653c5cf99ccb3df4380e258885d6652cf650968802e835c9c5b0bdf201c436ce4f89bc52b49b0885851faa006aad6668df3c0da042608bb20140fbd49202a1f1a71b4070b76e1518eda6cd00a4be93ab406056954d73fe9e320c87f38f18002aa763104ce008cd016cfda6a1162c3b287100cb5efed342ad5494956c156b0f2e5fd59a6e0064c208c4869bdb1c4ac0eba35c45fc8d5259e9588569367434f354515e5621991c1c1e7179111229b6e086fc8891ca095d9ca1803d1269c861de2a0a0d1390d8429e01b5c2f2477dad2a35e443562c8cb864c476668064b09a44a907fdf2834c381b74e15c201548405757a620215c16e03ab0477d3d47658c3e0c49a0010d44e2b0e716420d37d0f8a1416609fcd680ca0a73708c4084363c8623445997358cd106c4c83007e00da6458cc11a3a2a306dd28331432b16e59d14cdc4814ee26069970ee3ba4f1be9006e08c1509e3ed5d81846cd81c51274b32ec6d019f26be538f590531cc45b728b77a25d726573302063dd1b176a331de702e4a90529907cb59db820f957112ed4487be30237497146201e444b1f622776a82e431bcdd283116c33833301f0835978c23a112f7aaf105160087b6300be2ae8dc33360e9a4e3c186339502f6803c4ebdd35960e1b459fb3edb78e11a88b6698c501b810aee78671f3a57122f4e4c7f0bb30e832b1ca0ed341b8ed5138470038ad6d4fae3da98afa867373d4da949d2eaacc98ecc3e90249a179fa32d86c0a03b86dc9beccaeab7c9a17df5dbb679a5c573016a216920f869b9ef2240dee88545aec4657a4ccb0f62cc8839ba0b88e7db15a2bd5848365e424acee4a393b5687af3e24ab9b14ff7327d8076fdad4a2ed39b37a02cf9a5f52ca8a2d754924514fa280a6b322ad69100529f08ac045126dd731ee4ec05b73be281e88c64585c3aaaecaf360aa2f0e10eaf07f010876270087c35f6ae141e92ebb6878d4f87d0516a1de601c52fb543c0f09cb7151c2594812a3f86914d1549c68a2b05b4d054c193acd027885b1990458cb61e60097ae9107c27db687d52660e441b55fed21b18c8a0259d5277b1875be441e48fdcd81a62665a24053f3d541fed5a488c244508bed61b314893c3cf6c9110697654f01c695d943151321f230c5127b8852b6431ea454e480259fd35040922fe8040fe1285cc361b550b2180acb8652ea24f1723e4349f4e5e20eb0019ce53287354a4d79282c566ab13dec4217ab3aadfdba33ab167eeae7b46e81fb449b650b6938ccaad5a4c0e141341f1de0f0196d04587c8183b4f049bc0481e10b26b3e53ad8bb134973e375eb27ce885fd1429ed196ff5f02edcffcf7b9d9529f8ce0419a1f94984cac75a7d12e72ddc430b90b36de7418c97e57e5ed11ac93ea938be9cc65be108d66aee0ab5484e8c1b58bb880ae730b5941da0d2328aa95e5ba6bec2a26530e2de76ff730c66d3061c7c1d600d059c8f2c877383f733cfd9a68cc00b76ebf6153e335dd47ce02c63053d6cfd0732912048ba5fdec088b25415080b1ef3b294ca8c3beab305551bafd84098181efee85dc02e2e65e9b10018729240c100e5074091370786e22bb038281f9eebbca4519d9dc4f2c6010c32a89faae8660e823f737263e83a9fcc13ece61ca50f6ae673170e3ddf7a4b96f39271a6d9f33b3e7ac749b9163d9e9438f8b180400efb49bd8209bed745319db34a331c8c8b99d144ea472c86a7c340fc3715dbb63855e95b3c24aff782286981cbae13cb072cc978517161d56566e3baa708c5847f6e1b7609dc6148e391b4fd0cce3a944e4c8551a35c1be34bf9b881c160d2384e99434154137252d198bcc91c363aa2af3596d2d9ecdaf1fa870ac0f8a0a07d7bf451751581eb1d6155e0671784b05bb4a3c303f3e3c3a9ecfcea330c8aab82916f8f354beac691509747452440291d57a2137778f272aa064d94ac8a90364fc01436ac648eb11165c35e6d671cce0c11914552ff1e7205d7e0c52e0f6ec55bc22f767f37f954d9fceaefef9a169fd68f63aa563fd747638fb5d4d3cd2e264939587cfed57d2dc2fc5671710bc26ee0e454e2fd405461bcd53c1289c5e79f9db53022bf0ed8711a4b995335c5aac38c405d668201709a41d01b761367ee1b2b81b0dd0e3c78f5d274d1d875341cdc3f8a18bb4b55138d624db200744e2f4c39385e26859f8c4154b3924a7827e13baa32706e28858fe691ddcffd91535290cc70344210cc72f3c2f2c54e36dbac302026c70656a43ac1c58d31d352092a6fb5ad1c6d0b82ee575cbf2f3a3d955f63e0e7fdbd28277941bd28a2ecdacfed913bf124baa892d30cc263b607c7081d3ca0f88051f29d34ff011bbc8c2526c5b8aa6e26e9bab7d841b4ea8f24dba3d4b376864dbc8026bb74fe2683345d5581d37bec8ed31fef4c86a0e07ccecedd460c137f6f6bcddc68f8fb0d06d990e0f9dedf13dd2a25002977d1594213693fec4c46d48b847b67091f9bdcbc0a1c7b86a55e474d5357bb993f4f7167dbb032b0b4c58338705cc39ff3212ffb2b73370b7c69d8bc7f1b003e703729cc175920cf66c9de7f96f086c015a680357ac358c593825cc3c9f630a5478863da5dc7712adeab5c0e95616381066df16969a8a0ed8544dc718762cb6666f17991ddc148eb849769eee6aa4cd5eb99fba1db816444f70e0aa89bab1633a1ecfa0d6c5c359cc03359a493cf919a7aba7b6f3d07ddb578dbeafd2e426441e8751dfbeea9c078183d43c54ce836a3e0e99f36694f409f8fb386a5f9a00da51b3ae5b657b193b018bf29a6fb7bcee3d32708c98607d1f92b468dfb61e44574c30f0e3690aa791df1145a17bcfbbdbb8d94940159a399a14b80c8d1f292809b4e9ce7cfb7ddc3ccea6bcc66d3a0e6ef4b0c48a43a5deb35dbf3c898fe6fabe7f7bc37ea5d247f48f9f831eda3c484f909bb3dd0d2432b25b4946c321ed7df70cf363c88ff15eb9cbb67b0221f265897ca57234a23dd3419226b5696c2e71edd842269e984188d86404dd87654d7b236f07b592cdddbb9d59dca615a3d1963677199adc426a4ee40c56b5d3c2a6398b1bd9301ef31ccede2e9e7a735d0f3af4e0cc4e193340f2410e17a074df85c8e6f59e1db36a6a61821e9cd9596162b13b8830b1d2af4998b0d77b76549890770dfd2f484e46f6fe89809305bd2bcb12f00ecace2a922a520bd1230366c99e488d20cfcaeca816b1db674f2f50426c0380c6909ba3312d64cdeb283b2f443bbdbf9e528046db5d3b8bcfb407348664693b14edc0a7395770a83eef79cc83f6f5bb9d0b7b3025961b264a6a2a2118fb84c65e0a2636874d09f5903478a0350a59a25fb1756c780571374c64ecb5d10184ebb9f8605a136323e7fe93c7943d6c574f882666b28df754c229ab07d2cee6ab9be2919f2a66940bf7541590d447adbcd42eea12b0876cf38ae4d5243176c1d656b50756007680241795813761420af4a60402dfa42731c16f8f26940eda22a8073c4fb0dc05331615f8ec3b041c49430b42e61cd070175c05b42f3cabbcd229704aacf40bd4d14b1992815bd7373b54d4f6cdeae8fb46f25eebfaae54adb6ebaa8abe6738772a3cbf2a9301996355213ecfc00cb8288506ea4c94d95125392ad1d9679a25f6f350dcbb61fad0d41fac13b5d9af79758d67c0d6267855ecd38638ee9bb2f04131fb7cc3e6a36c8e38a73496e2d001327591eae0ee8d47947dd21028ad13428a288fe4e1d9abb5b1d55ec6701c12a500610da5ba50e2ae8843edaad0e141885682600172f581b288f383c3b6fda89b9fb642df814830681320d41cdc099dd07118b32f1a32618305ca6ee39d6c6cde1aa311bdccdd310947c2e274a2ad7d30db8b8083cb3df074bb7ff2ebfcf566f2e1b7b97b8afc7884cba1473a5bc3a8ca3a1b1a032a3229cc46431f9af9a92f79e8e607c9f3e48fe87a9782138d6628f249b4b0eb42f211f9239a33ee71ba75391bfb92eea4f07a10caf64c3891800fdffbd8f2fb3e38a7903f828d6b93552e071f0a0bd815aab927bc138eaf4a5609237a2e4aa311adf8ec2082f58e3d0f9b43f504454ec3e497547c97687245795c8387251770a600e49a5c2e1aa70ba0862015348a325332a43465a78beaa0887da03f954c28a78bb7dbb8788bb6faf58c64e15d0be294c28cc95270303475aee2dba4f6734818d555a4372a5e923c580579709ee6e16db0cc6971f12278b9b1281f732bdea5bf21ababf8f536df6c734a3259df44c21150e12fd1f57fba50703e7d5d3e1999f92081a21916cff7be8e7fde86d1aac1fb1278280301513862d80b4cc558e6c54b4c770f0da457496c0988b1aff11fbd23eb4d448165afe3ebe033e9821b15bf17e42e583eb42fbf6140cc0321b2fdf45918dca5c13a6330daf6f42795e1d5fafec7ff01a948526c20ff0000	6.2.0-61023	T
201809142212596_AddIsDeletedToQuestionAndAnswer	iKnow.Persistence.Migrations.Configuration	\\x1f8b0800000000000400ed1ddb6edcb8f5bd40ff61304f6d91f5f8b209b6c178175e276e8d8d933476167d0be419da11a29166254d62a3d82feb433fa9bf504aa228de0e2f12759934f08b47140fcf8d878787473cfffdf77f963f3d6ca2d967946661129fce8f0e0ee73314af927518df9fce77f9dd773fcc7ffaf18f7f58be5c6f1e66bfd6ef9d14efe19e71763aff98e7dbe78b45b6fa88364176b0095769922577f9c12ad92c8275b2383e3cfcebe2e8688130883986359b2ddfede23cdca0f207fe799ec42bb4cd77417495ac519491e7b8e5ba843a7b1d6c50b60d56e8741efe12275f0ede169864394615cd676751186044ae5174379f05719ce4418ed17cfe3e43d7799ac4f7d75bfc20886e1eb708bf7717441922e83f6f5eb7a5e4f0b8a064d174ac41ad76599e6c1c011e9d10d62cc4eead183ca7acc3cc7b89999c3f1654970c3c9d9fadf2f0337e349f89833d3f8fd2e2c59abfe7498a0e2a611cd4bd9ecccab627540db0b6147f4f66e7bb28dfa5e83446bb3c0da227b3b7bbdb285cfd821e6f924f283e8d7751c4628671c36ddc03fce86d9a6c519a3fbe437704dfcbf57cb6e0fb2dc48eb41bd3a7a2e432ce4f8ee7b3d778f0e0364254f00cd5d739a6f36f28466990a3f5db20cf511a173050c93a697461acb3ed16eb58da0c89d50d4f9cf9ec2a787885e2fbfc239e52c73fcc6717e1035ad74f081aefe310cf33dc294f77c83452019e0e82f59c0aa56a906834404bb6e1cacc283d907fec505630b12b9cb338fbc2f2b01d9417588037d8a4d4509adf2640af83cfe17da90e6ae9ce67ef5054b6671fc36dcdfeaaed03114388322ce434d9bc4ba2a627d3fae12648ef518eb14bc057ae935dba12f05b2e9a49ac9fda35b24e33bbeab46f13bbc32c73d4aa8b30cdf2e25fcdd04f0ffb18f95530d2c078faa58966d4e3a74f3dd8336c73d785b656c3fcfc98b7e050b222eb64570e192c0bba0bf034b85c25f1ebdde6b623d62f37411869d9fbcc03cee528d8abba0bd30da2f3e6e7049b9e2076c6f96d90655f9274fdf720fba8411dffeb01f56bb4daa5d8b65de7c166dbfb686f3f2631e2c53ac458de4473f325b90856d88b791917bd3ac3c393ea53b2cb5fc6eb62017d9fafe4f5d412801774ce562b9465175899d1fa3cc19b866e4e42b1d8198caaedfc6bef55301e830fc7a2f61a348e45ed7b58a3587a6406fcea7714c8554d3066a4dd15adf32808377aacea5764a4aa161027d2ec8ad2451245c917ac436ab44a3f9bbef3a172d01adc14cd929fa87a47e528eab07c95dc87b19e71f52b32e3aa169071a4d99571f5e6418f15f3968c186d04716bde7045af80a3478dbc21a35536802855adaee8944aa0c7a77e4546a86a013122cd2a94ec371fe58c76dc7b947df66debd17f4c01bb06396a16ba0ede88e3e2789ea202e162a177df448b0bed760dc31a28ace03136e3ba85ca5ea008e52ddc1fc392fc2afc049925dafea19e8c8c25101be56884f486eb1a63152c31fb0c609844f029ac7d8664b3c1b349cd34d2a8e018df22612534bb2255abb812a9ba51c52db14d424c7aa15b4c896a451bd35ef4fb66dec70b19b78cab1a4c900feb23b922a07df2627d18e8f56b6aec88add1a1573b4d5dbc25623ddce614e9f46d427d95fe929f3390e16ccb3bb48db05e11a594f0f66052acd76671b6024bb70f4b4241cb66446882b1d21810ed869463b8163bf1551949fe0d1057e1b54e36af71795c8c5eddeb9bd593e2be611ee9c39876a734aea7ac285ba5e1d670ec727478e8e3e0e56bdcc1a937224e3ebf385dc14d81cfad9b65240edabec9b13a0fa12f4ab81cfb129a609ef9887e9530dc0c5bd9e59b5513c71ae5c0bb5f9b669a751da3bad08413f4be9ff30ca2f8e08146f9d374a251bdd43684d2d9328011140d07dd2c0325b58589a07d87b715f5d2bb30bf4ad3c95c6dcb58cb7bbbfc37fd1ae56fc2888b957656d9e208aef02dce280d08768f90d4eb4501a93c99d54d9d2b9a0f7b966d5fa3fca0ee7d50c1bd4831cc2f49fae94002fb6466ddb99979c7b633efe4e8f6eee487a7cf82f5c9b3efd1c9d3af71c51e6b0697e2e39262fbcada2947fa358876be876a351bcae376ffb3a1043bfdd950a2891f7f0ecbe4418ba5a97e1983b77a5fbdea99e79c80d9d0d3812373e8c187b101ada64bb13ef99f2d05d4e94f160707ae20689ffcb71adf89699c7f6ddb0f4ddb976cfeb1533fe1fd3e2bef0fe4b5c607975b25175cf18a9b07bedb28be13ab1caccbec220aee9b0fe7da7c3b56b477dcc36231e0152e7ac46263bd5b9ef757a848afe6a21964d355ba7058b52471715daa186a735a417ac9675a7caff55aea72a2ef521c27d7675ba4c7f7b2ac2aa9b00fcfb22c598525dff940129b31cc8ffb325ecfcce9c3cca765753cf80a8b25dc624160019ece0f0f0e64e66961d3d88afcd99a08fc2f12646c5e505a2875507cc89061bd08e35cb64561bc0ab741642450e86969c70a29d031c49617685b7cc512e7460ed80cce9c76c838d0a1042b6b62d272c1e88b418da41c0e50d270420723682677c8246a1d68950e9169c3833d12b9bc7c13578738b34220c5e7b8e741b60ad6b2c1c6136ded43f920e487d03d482656aa47b30f46d73c627a6cf4433c69f7a97bc2297d4be3d8598b78340656239ebffb60c2849c1148d050024923649a3b61af3d50c6e8f4cd961af301944d2d873d3058621e9051276053d545d3463452000e432acdfe9a272141cb2467285bcb8b1201395e66e0fe75498dca802aa566b40d02723ae4280a26a51141d287738a1ab9371bda1e172c0d4ad62ba8a7ad23c493011410a2dd6668f673a951944e79c40d49597fdedd485a4c9cb0376cfa14146184e939633af407d0449d7c6c86073241c65145ad6fa6cd6be841111d5db5c969e2c06e9e463c36a38feae789d967c6254f4c52352fc23ac50333dc0cc64f80e912c011beef34868861af039c0e1681e7911c0780aa2103ceee6ec3d87b21e1c20e936cc5db3b86b59f203e0a5553a4aef5aa753c6706543a9e059337cac2452726018bb79e8cad70c2352b80c291ecb05e158ee7cc800ac7b3606f14aec95437c95871a18d978551f1794e1b0fa393d648b40da83812fdfbb44056f70c99442c5c3a34b6b5e26f39028c55952dd5abd6716c1950e338faf7c65219b62bd07d524665d31eba029f33f5b65751645a41b8e9d2ae649556a9734fd34c93ec35fc5c83b934c08483396133b83aedb5975957a5dd157755e01e3409accc872b2fb078505d07820922a97519c93d1435a2007a8d723e6dafbc33b2c9f39332afa4c9c84321e98732807a761bfad36f986508641b6e0580dc750500a9923b0c809acb9f2428f454cb0082f9ce5082d1b84e0620f55788120462db6cbab35f66aae130514a0340698faa82a9d8c83a80adef93d48225db1507b09537a4875ad93a035013202510666e33daaab8b0957911bed655343a56699a940e3a4f25e3659592c9c061111717219e621b6ec837412998a14f35e46900930d5912588ba16308985e2801f3cd8cda78eab9a14a28012910b24a3af243c82631295a0b768857fac8acd02570718803295c0cd28dedd7d00f246df5a20cd2e5411af28d6a006516b566c070d217af2782d9a0cb9351520164cab4650a901b6301ae057be42b6064c6e8f33bd48740e2590b833de3db68b8012666f4324bd41fcccbac30671d704468f30e184224574bc3186da68108b307de4056c4780aaea302b2261d18d3b761912efbd04c1c558843aded4294a3fdb411221b7d288674bfaec6ff345a0ee890b69de73984cd102fff87a9579d36eacef73ad12e9c11323054fb2f6f5ca8775e3017544760368760adb8201c5c015ca891f6c6056692c28c008e662c0f675ab1433e8bb1b12c1d184136ba301314070c16470cad88e78f05005520087b6300bc2ae8e2de3691ef563c186039507d4c2c536f0a84db86c2190a94a2b38d5c7b5287fa0b671a6aa56dcb45554c943c582e80aaa3cbab60bb2d9c9ca6277932bbae4a909e7f77ed5e9c7353c158f09a260686e948799206f748682d761c6b5416a57b11e4c16d507c8e7dbede48af7181652012560f25c58e65f1d541b2ba4bf13f13c13e78db54636de2cc72049e74bfc09415dba692482447a2145d674525d8200a52c52d02e749b4dbc4f07102dc9b39e36581688e7e6158d5a7f22c98ea8903843a7796030225d4c270d88c7016942e535cc323faa915c722f0032c18527339360b092a0b52c259081a239dd348aa291da2f1ca6e35152063e8340bd4211c9b4900f5ec670e30152e5920cc637b584dcd4a1654f3d41e122942c991553db2875197986481d4cf1c68a25526399ae85307fd97eb487213416eb6874daa4ab2f0c82347184c61420918d3660f95af1dc9c2e45bec210a05225990429303966c19480e49b6a1153c80a3ea371c560ba9f023b76c48ad4e1a2f968014545f6c6e015b81b3d8e6b046c95522b9c54a6eb6875dd862d9a6354f27b36ac1911da7754b79f262b36c011dfb59b568d10f16047de80087ade1c1c1621b1cb485ad7bc6290cdb309a2fe7d1df656ec8e7c4d83c9ed8cca84e437dcc8e32f7a4ed0c51779efefec67d1f3092b4e9995c375103a78e1672067bfe7f99427f1b479f6a2c7fa9cf82347fc73f9a5aeb62d52e7a4d93d6dc151beeda8f66df54356e38bfb67ae4b2e9628a3bf0db2da6e1db12ca2b93a47d6088dc45f59487f4167a07f4eb47e9645fdf3576d156e5c6142d73b2ef41c64d266a4b616b00e8f669a2e45b44711d63b023c94c7180dc4d6c72b2afbbe42c60f43365fd889ea928c0793fcd634758a46680048c3c9fa43281a9016d95a94af1eea64c000c38c6c45dc5cf8798b4f5036098dcfdfa5c184f575f0086e7a6b213500c284ba0ad5e94f9f5ddd4420da25f23517f31c46d1a80af8846939b4f597594939b8c86f2dcfa960b9f9ca0dec7d1bc149bcd1a7d19da9281f9af403c514865913964251fcd6d4fccd0ee58819ff05961a5bf110d424c4c297116ac986f64713a0c8a95b4db4a559d9fd4927df0a7c84e3255e73b0da76866794a9942e22bd44c9027f437cd1422593a5cfa504953910c54d292918c21316da77a653eabfd87d3f9f523568ecd41f1c2c1f56fd179149601bcfa85ab200eefb062570511e6c78747c7f3d95914065995cf4512929e8b1f915a65281d9d14194a68bd5988ddddf39c0a2859b6e66a0eca2553d4a93e43941b090bae1aab9e75adbc1e7f0ed2d5c720557cd57b19afd1c3e9fc5f65d7e7b3cb7f7ea0bd9fccdea458d6cf6787b3dfe582280d4e3675cbd8e26825cddd6a24b601c15ae2f6509a90717b184d965105a3388ccbcbdf7e2a0029937686d0e646cf606db1e21093f0a3815c54e07504dca4fff8854bf2813440cb1ae86e93a6ce0faaa0e661fcd846db9aec206b926d9053640875c393a4086959f8cc154b3155a8827e1bbaa3c72708f158fe69133cfcd91535213dc803442e3dc82f3c2f2c94f380dac35224fec0c6d4865831e1a73d6a8a0c9ff66b4593dbe3ba94d73dcbc74f6697d9fb38fc6d871b6e303784155d98599d161f55b98b7df5a4e8c9b56136d901638fae9d567e855ab0193cdd141ff08b2c3cc5a627ef2a4edb5db55bcf9b634f5b43e03843e4d49bbd9d2513dc6f28dd741b64483f0785b696bbbac4c6be0a7db2a67140d1efc55490d388dcf822f687f8d3a1a6b83a97676fa706c90bb2df0cda450dd8840ddd7efbf0d07933f7ffb604abea82ecabb6f511cef0a76b6e22616e8d83f5eef73682030f12e45781f8be6b0172a729d4c3fc51854c2d3021ddfa70800c39397b3b03a725772647c8430c884d127206d74a33c83d8c9ee7bf21d946d1439b4c636d61ccca2960e63992ce51e119f6987adf4ab5aa7b34c75b59d4c939fbb6b0d454b4c0a6ea3aa4d8e59c9fbd3a086b17fa2d881e21f4abc9ffb1633a9c5921bf0b27d6980535986b347ab4d5f5ccb8b5e8bef9d783fbd79ad267c0f539f20d50ad0b353390e855ee2c28fab0cf921a8354d1806f1092c7d27c79354cf10cfd5dd6d265cb439784575df6acc0a17adc8fe640570af8571cdde7d90ad519b78abce9deef7e4cc7584a30a0f970d4828954859fb8f5a0f775330858d79d9fb8dd80bff59f9cd1d05e11df8bc51841f0c3d90a27c94fc450e8aec56f27373b0da8b2cc07d30217d1f8d18292409be1ccd7440c5b67da5477b92954c4480f2afcd8abf7395c5566cd3d173d791c5623e93f4e1a4069cc8513c49bfccdd5f87a5219f17c52448369ea4585a0fb2abceb8ff1d20497b8dd084ae4cb13f94af568407fa685268dead3d87c8f3ab1858c0fb9ab10b1a958ba0fcb9af6e3e2095a259bcf8827b3b88dab46832d6dee3a34ba874443fa06afda69613345e486738c078fc4edc5e6ba16baea36a54939338ab2ac0c2e8ad67d57229baba926e6d5d4caa4ba4d69b2ca4492c0006522ad5f93324157534d5499800b40fd2f484e4ef6fea98093073d95654971a5d3640d4995f207d891aa71df75c8e226b3895a11bb7df6f80ac5254729d0e8737334a487acb9e869f24a34e9fdf5980a34d8eeda597dc60dd0984a0e028502958b896a21f98a1737c37d8cd358e1a09b307b50ae97fcd56df4885cac8e28ca945cb1571b349a33d9e47b4ae994d5556da7f3f56d71dd509533caa47bca8eb43046bd6eca43d42dca11b2ed6b945793c438048917c9239006e50040fd6535f02a350c18a06a8407a94aaf9806a2e904d228b4453504adf76182df6c61a4019a26d50870595e7108b2a848f0c9731570a0eaab12327350a51e8279011c0b2ee22e0daa882649e32aded1ab3350f05a3736093e68c726efe8c706ca4cebc6ae6cba76e8ea15fdc8ea32b6d0c086414d03da0d269c338093c2a4bcf6d38377bd207b681a4f6d13b535a059730dd781d696c095b6469438e699b4f0a972f6d98ef4a1e88eb897bb96f2d05594ea93d595e7122cbae4918e4c69b5106b5ff925b5e6bc9e56f586ce875407205748105690aa4b21f620536161e68a5c79240f16a536e9d58b1c872151c8dcd450aacbf16c8bb8aa5f95d3d90bd15276a2825c7d0623109566b06d1e1ae7a885d2b72052994da720d49c75a70a9d301893271a32d51ea2aaa68e77b2a1796b4c13f3327787241cc857d2a9b6f652762f0aae74e414e501fc934f228016e4abef7fefa8f2c3112ee684e8bc48a329ebe66cf463c884fc070d7d60bda9aee481bb4da0ba943fa2eb6d214c345817c927d1dc3617a882e48f6866db06d3adab3ad995742783d78150b21b8689541cae7a972dbba3575732f247b0716db2aa17e2c36029f6fb727d13ef84c3ab925551928e8bd250442b0e5314149b8e5c34872e0a655529ea202a2fd56aa16dcb451528220ff04fa926cb72f16e1717179b56bf5ea02cbc6f402c31cc18adb80306face657c97d4e71c0246f52bc21d1557280fd6411e9ca5797817ac72dc5cdc4d5eba9fe5a55ec50df9b7687d19bfd9e5db5d8e49469bdb880b0115e725baf1970b09e7e59bf2eac0cc070918cdb0b80bf64dfcf32e8cd614ef0bc545190088e220865cf756c8322fae7dbb7fa4905e27b12520c23e7a7e748336db0803cbdec4d7c167d40637ac7eafd07db07a6c6e0083809805c1b37df9220ceed3609311184d7ffc13ebf07af3f0e3ff003fcea65f75010100	6.2.0-61023	T
\.


--
-- Name: Activities_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Activities_Id_seq"', 100, true);


--
-- Name: AnswerLikes_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AnswerLikes_Id_seq"', 18, true);


--
-- Name: Answers_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Answers_Id_seq"', 77, true);


--
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AspNetRoleClaims_Id_seq"', 1, false);


--
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AspNetUserClaims_Id_seq"', 1, false);


--
-- Name: Comments_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Comments_Id_seq"', 2, true);


--
-- Name: Questions_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Questions_Id_seq"', 61, true);


--
-- Name: Topics_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Topics_Id_seq"', 44, true);


--
-- Name: AspNetRoleClaims PK_AspNetRoleClaims; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetRoleClaims"
    ADD CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id");


--
-- Name: AspNetUserTokens PK_AspNetUserTokens; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserTokens"
    ADD CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: Activities PK_dbo_Activities; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Activities"
    ADD CONSTRAINT "PK_dbo_Activities" PRIMARY KEY ("Id");


--
-- Name: AnswerLikes PK_dbo_AnswerLikes; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnswerLikes"
    ADD CONSTRAINT "PK_dbo_AnswerLikes" PRIMARY KEY ("Id");


--
-- Name: Answers PK_dbo_Answers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Answers"
    ADD CONSTRAINT "PK_dbo_Answers" PRIMARY KEY ("Id");


--
-- Name: AspNetRoles PK_dbo_AspNetRoles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetRoles"
    ADD CONSTRAINT "PK_dbo_AspNetRoles" PRIMARY KEY ("Id");


--
-- Name: AspNetUserClaims PK_dbo_AspNetUserClaims; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserClaims"
    ADD CONSTRAINT "PK_dbo_AspNetUserClaims" PRIMARY KEY ("Id");


--
-- Name: AspNetUserLogins PK_dbo_AspNetUserLogins; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserLogins"
    ADD CONSTRAINT "PK_dbo_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey", "UserId");


--
-- Name: AspNetUserRoles PK_dbo_AspNetUserRoles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "PK_dbo_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId");


--
-- Name: AspNetUsers PK_dbo_AspNetUsers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUsers"
    ADD CONSTRAINT "PK_dbo_AspNetUsers" PRIMARY KEY ("Id");


--
-- Name: Comments PK_dbo_Comments; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Comments"
    ADD CONSTRAINT "PK_dbo_Comments" PRIMARY KEY ("Id");


--
-- Name: Questions PK_dbo_Questions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Questions"
    ADD CONSTRAINT "PK_dbo_Questions" PRIMARY KEY ("Id");


--
-- Name: TopicFollowings PK_dbo_TopicFollowings; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TopicFollowings"
    ADD CONSTRAINT "PK_dbo_TopicFollowings" PRIMARY KEY ("UserId", "TopicId");


--
-- Name: TopicQuestions PK_dbo_TopicQuestions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TopicQuestions"
    ADD CONSTRAINT "PK_dbo_TopicQuestions" PRIMARY KEY ("QuestionId", "TopicId");


--
-- Name: TopicUsers PK_dbo_TopicUsers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TopicUsers"
    ADD CONSTRAINT "PK_dbo_TopicUsers" PRIMARY KEY ("UserId", "TopicId");


--
-- Name: Topics PK_dbo_Topics; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Topics"
    ADD CONSTRAINT "PK_dbo_Topics" PRIMARY KEY ("Id");


--
-- Name: __MigrationHistory PK_dbo___MigrationHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__MigrationHistory"
    ADD CONSTRAINT "PK_dbo___MigrationHistory" PRIMARY KEY ("MigrationId", "ContextKey");


--
-- Name: IX_AnswerId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AnswerId" ON public."Comments" USING btree ("AnswerId");


--
-- Name: IX_AppUserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AppUserId" ON public."Activities" USING btree ("AppUserId");


--
-- Name: IX_AspNetRoleClaims_RoleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON public."AspNetRoleClaims" USING btree ("RoleId");


--
-- Name: IX_QuestionId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_QuestionId" ON public."Answers" USING btree ("QuestionId");


--
-- Name: IX_TopicId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_TopicId" ON public."TopicQuestions" USING btree ("QuestionId");


--
-- Name: IX_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_UserId" ON public."AspNetUserClaims" USING btree ("UserId");


--
-- Name: RoleNameIndex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "RoleNameIndex" ON public."AspNetRoles" USING btree ("Name");


--
-- Name: UserNameIndex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "UserNameIndex" ON public."AspNetUsers" USING btree ("UserName");


--
-- Name: AspNetRoleClaims FK_AspNetRoleClaims_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetRoleClaims"
    ADD CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AspNetRoles"("Id") ON DELETE CASCADE;


--
-- Name: Activities FK_dbo_Activities_dbo_AspNetUsers_AppUserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Activities"
    ADD CONSTRAINT "FK_dbo_Activities_dbo_AspNetUsers_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES public."AspNetUsers"("Id");


--
-- Name: AnswerLikes FK_dbo_AnswerLikes_dbo_Answers_AnswerId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnswerLikes"
    ADD CONSTRAINT "FK_dbo_AnswerLikes_dbo_Answers_AnswerId" FOREIGN KEY ("AnswerId") REFERENCES public."Answers"("Id") ON DELETE CASCADE;


--
-- Name: AnswerLikes FK_dbo_AnswerLikes_dbo_AspNetUsers_AppUserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnswerLikes"
    ADD CONSTRAINT "FK_dbo_AnswerLikes_dbo_AspNetUsers_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES public."AspNetUsers"("Id");


--
-- Name: Answers FK_dbo_Answers_dbo_AspNetUsers_AppUserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Answers"
    ADD CONSTRAINT "FK_dbo_Answers_dbo_AspNetUsers_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES public."AspNetUsers"("Id");


--
-- Name: Answers FK_dbo_Answers_dbo_Questions_QuestionId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Answers"
    ADD CONSTRAINT "FK_dbo_Answers_dbo_Questions_QuestionId" FOREIGN KEY ("QuestionId") REFERENCES public."Questions"("Id") ON DELETE CASCADE;


--
-- Name: AspNetUserClaims FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserClaims"
    ADD CONSTRAINT "FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- Name: AspNetUserLogins FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserLogins"
    ADD CONSTRAINT "FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- Name: AspNetUserRoles FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AspNetRoles"("Id") ON DELETE CASCADE;


--
-- Name: AspNetUserRoles FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- Name: Comments FK_dbo_Comments_dbo_Answers_AnswerId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Comments"
    ADD CONSTRAINT "FK_dbo_Comments_dbo_Answers_AnswerId" FOREIGN KEY ("AnswerId") REFERENCES public."Answers"("Id") ON DELETE CASCADE;


--
-- Name: Comments FK_dbo_Comments_dbo_AspNetUsers_AppUserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Comments"
    ADD CONSTRAINT "FK_dbo_Comments_dbo_AspNetUsers_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES public."AspNetUsers"("Id");


--
-- Name: Comments FK_dbo_Comments_dbo_Comments_ReplyToCommentId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Comments"
    ADD CONSTRAINT "FK_dbo_Comments_dbo_Comments_ReplyToCommentId" FOREIGN KEY ("ReplyToCommentId") REFERENCES public."Comments"("Id");


--
-- Name: TopicFollowings FK_dbo_Followings_dbo_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TopicFollowings"
    ADD CONSTRAINT "FK_dbo_Followings_dbo_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- Name: TopicFollowings FK_dbo_Followings_dbo_Topics_TopicId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TopicFollowings"
    ADD CONSTRAINT "FK_dbo_Followings_dbo_Topics_TopicId" FOREIGN KEY ("TopicId") REFERENCES public."Topics"("Id") ON DELETE CASCADE;


--
-- Name: Questions FK_dbo_Questions_dbo_AspNetUsers_AppUserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Questions"
    ADD CONSTRAINT "FK_dbo_Questions_dbo_AspNetUsers_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES public."AspNetUsers"("Id");


--
-- Name: TopicQuestions FK_dbo_TopicQuestions_dbo_Questions_TopicId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TopicQuestions"
    ADD CONSTRAINT "FK_dbo_TopicQuestions_dbo_Questions_TopicId" FOREIGN KEY ("QuestionId") REFERENCES public."Questions"("Id") ON DELETE CASCADE;


--
-- Name: TopicQuestions FK_dbo_TopicQuestions_dbo_Topics_QuestionId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TopicQuestions"
    ADD CONSTRAINT "FK_dbo_TopicQuestions_dbo_Topics_QuestionId" FOREIGN KEY ("TopicId") REFERENCES public."Topics"("Id") ON DELETE CASCADE;


--
-- Name: TopicUsers FK_dbo_TopicUsers_dbo_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TopicUsers"
    ADD CONSTRAINT "FK_dbo_TopicUsers_dbo_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- Name: TopicUsers FK_dbo_TopicUsers_dbo_Topics_TopicId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TopicUsers"
    ADD CONSTRAINT "FK_dbo_TopicUsers_dbo_Topics_TopicId" FOREIGN KEY ("TopicId") REFERENCES public."Topics"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

