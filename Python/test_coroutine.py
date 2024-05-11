import asyncio

async def count_to_three(additinal_text):
    print(f"Веду отсчёт. 1 {additinal_text}")
    await asyncio.sleep(0)
    print(f"Веду отсчёт. 2 {additinal_text}")
    await asyncio.sleep(0)
    print(f"Веду отсчёт. 3 {additinal_text}")
    await asyncio.sleep(0)

coroutine_counter = count_to_three("Дарова")
print(coroutine_counter)  # <coroutine object count_to_three at 0x7f5a58486a98>
coroutine_counter.send(None)  # Выведет "Веду отсчёт. 1"
coroutine_counter.send(None)  # Выведет "Веду отсчёт. 2"
coroutine_counter.send(None)  # Выведет "Веду отсчёт. 3"