CREATE TABLE IF NOT EXISTS products (
    id VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    price DECIMAL(10,2) NOT NULL,

    PRIMARY KEY(id)
);

CREATE TABLE IF NOT EXISTS reviews (
    id VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    rating DOUBLE NOT NULL,
    productid VARCHAR(255) NOT NULL,

    PRIMARY KEY(id),
    FOREIGN KEY(productid)
        REFERENCES products(id)
);

CREATE TABLE IF NOT EXISTS orders (
    id VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    canceled TINYINT,
    shipped TINYINT,
    orderin DATETIME NOT NULL,
    ordershipped DATETIME,
    ordercanceled DATETIME,

    PRIMARY KEY(id)
);

CREATE TABLE IF NOT EXISTS orderproducts (
    id VARCHAR(255) NOT NULL,
    orderid VARCHAR(255) NOT NULL,
    productid VARCHAR(255) NOT NULL,
    
    PRIMARY KEY(id),
    FOREIGN KEY(orderid)
        REFERENCES orders(id),
    FOREIGN KEY(productid)
        REFERENCES products(id)
);