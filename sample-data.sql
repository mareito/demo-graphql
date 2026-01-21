-- Crear categorías de ejemplo
INSERT INTO "Categories" ("Name", "Description", "CreatedAt")
VALUES 
  ('Electrónica', 'Dispositivos electrónicos y accesorios', NOW()),
  ('Ropa', 'Prendas de vestir y accesorios de moda', NOW()),
  ('Alimentos', 'Productos alimenticios y bebidas', NOW()),
  ('Hogar', 'Artículos para el hogar y decoración', NOW()),
  ('Deportes', 'Equipamiento deportivo y fitness', NOW());

-- Crear productos de ejemplo
INSERT INTO "Products" ("Name", "Description", "Price", "Stock", "CategoryId", "CreatedAt")
VALUES 
  ('Laptop Dell XPS 13', 'Laptop ultradelgada con procesador Intel i7', 1299.99, 15, 1, NOW()),
  ('iPhone 15 Pro', 'Smartphone Apple con cámara de 48MP', 999.99, 25, 1, NOW()),
  ('Camiseta Nike', 'Camiseta deportiva de algodón', 29.99, 100, 2, NOW()),
  ('Jeans Levis 501', 'Jeans clásicos de mezclilla', 79.99, 50, 2, NOW()),
  ('Café Orgánico 1kg', 'Café de grano entero orgánico', 15.99, 200, 3, NOW());
