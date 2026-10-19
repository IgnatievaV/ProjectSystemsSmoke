// Unresolved import on purpose: check the TS error and that Rider does not create files for it.
import { Dog } from "../TestProject333/test/Dog";

const dog = new Dog("Bob");
dog.bark();
