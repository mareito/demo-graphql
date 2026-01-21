import axios from 'axios';

const GRAPHQL_ENDPOINT = 'https://localhost:7018/graphql';

export const graphqlClient = {
  async query(query, variables = {}) {
    try {
      const response = await axios.post(GRAPHQL_ENDPOINT, {
        query,
        variables
      });

      if (response.data.errors) {
        throw new Error(response.data.errors[0].message);
      }

      return response.data.data;
    } catch (error) {
      console.error('GraphQL Error:', error);
      throw error;
    }
  }
};

export const GET_PRODUCTS_QUERY = `
  query GetProducts {
    getProducts {
      id
      name
      description
      price
      stock
      categoryId
      category {
        id
        name
        description
      }
    }
  }
`;

export const ADD_PRODUCT_MUTATION = `
  mutation AddProduct($input: AddProductInput!) {
    addProduct(input: $input) {
      id
      name
      description
      price
      stock
      categoryId
      category {
        id
        name
        description
      }
    }
  }
`;
