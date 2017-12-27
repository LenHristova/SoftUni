package shoppinglist.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import shoppinglist.entity.Product;

import java.util.List;

public interface ProductRepository extends JpaRepository<Product, Integer> {
}
