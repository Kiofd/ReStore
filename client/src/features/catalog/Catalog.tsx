import ProductList from "./ProductList";
import {useEffect} from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import {useAppDispatch, useAppSelector} from "../../store/configureStore";
import {fetchProductsAsync, productSelectors} from "./catalogSlice";

export default function Catalog() {
    const products = useAppSelector(productSelectors.selectAll)
    const {productsLoaded} = useAppSelector(state => state.catalog)
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!productsLoaded) dispatch(fetchProductsAsync());
    }, [dispatch, productsLoaded]);

    if (status.includes('pending')) return <LoadingComponent message='Loading products...'/>

    return (
        <>
            <ProductList products={products}/>
        </>
    )
}