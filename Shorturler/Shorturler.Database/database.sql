create table short_urls
(
    id       uuid          not null primary key,
    full_url text not null
);

create index idx_short_urls_full_url_hash on short_urls using HASH (full_url);