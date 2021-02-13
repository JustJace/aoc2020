import { performance } from 'perf_hooks';

export function timeAndPrint<T>(fn: Function) {
    const start = performance.now();
    const answer = fn();
    const end = performance.now();
    console.log(`${fn.name.padEnd(4)} ${(end - start).toString().padEnd(20)}ms ->`, answer);
}
