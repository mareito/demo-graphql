import { useState, useEffect } from 'react';
import ProductList from './components/ProductList';
import AddProductModal from './components/AddProductModal';
import { graphqlClient, GET_PRODUCTS_QUERY, ADD_PRODUCT_MUTATION } from './services/graphqlClient';
import './index.css';

function App() {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);

    const fetchProducts = async () => {
        try {
            setLoading(true);
            const data = await graphqlClient.query(GET_PRODUCTS_QUERY);
            setProducts(data.getProducts);
        } catch (error) {
            console.error('Error fetching products:', error);
            alert('Error al cargar los productos');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchProducts();
    }, []);

    const handleAddProduct = async (productData) => {
        try {
            await graphqlClient.query(ADD_PRODUCT_MUTATION, {
                input: productData
            });

            // Refresh the product list
            await fetchProducts();
        } catch (error) {
            console.error('Error adding product:', error);
            throw error;
        }
    };

    return (
        <div className="min-h-screen bg-gray-100">
            <div className="container mx-auto px-4 py-8">
                <div className="mb-8">
                    <div className="flex justify-between items-center">
                        <div>
                            <h1 className="text-4xl font-bold text-gray-800 mb-2">
                                Gestión de Productos
                            </h1>
                            <p className="text-gray-600">
                                Sistema de administración de productos y categorías
                            </p>
                        </div>
                        <button
                            onClick={() => setIsModalOpen(true)}
                            className="px-6 py-3 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors shadow-md hover:shadow-lg font-medium"
                        >
                            + Añadir Producto
                        </button>
                    </div>
                </div>

                <div className="bg-white rounded-lg shadow-md p-6">
                    <ProductList products={products} loading={loading} />
                </div>

                <AddProductModal
                    isOpen={isModalOpen}
                    onClose={() => setIsModalOpen(false)}
                    onProductAdded={handleAddProduct}
                />
            </div>
        </div>
    );
}

export default App;
