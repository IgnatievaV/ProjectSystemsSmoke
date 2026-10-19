import { expect, test } from "vitest";
import { formatGreeting } from "./greeting";

test("formatGreeting prefixes the text", () => {
  expect(formatGreeting("hi")).toBe("Backend says: hi");
});
