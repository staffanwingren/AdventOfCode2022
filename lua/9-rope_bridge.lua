local head_movements = { 'U 1', 'R 2', 'D 3', 'L 4' }
local head_position = { x = 1, y = 1 }
local tail_position = head_position

local function move(from, direction, length)
    local new_position = from
    if direction == 'U' then
        new_position.y = from.y + length
    elseif direction == 'R' then
        new_position.x = from.x + length
    elseif direction == 'D' then
        new_position.y = from.y - length
    elseif direction == 'L' then
        new_position.x = from.x - length
    end

    return new_position
end

local function parse_movement(input)
    local direction = string.sub(input, 1, 1)
    local length = tonumber(string.sub(input, 3))
    return { direction = direction, length = length }
end

local function follow(head, tail)
    local y_diff = head.y - tail.y
    local x_diff = head.x - tail.x
    local new_position = tail
    if (math.abs(y_diff) > 1) then
        --TODO
    end
end



for _, v in ipairs(head_movements) do
    print(string.format(
        "head:%d,%d tail:%d,%d",
        head_position.x,
        head_position.y,
        tail_position.x,
        tail_position.y))
    local movement = parse_movement(v)
    local new_position = move(head_position, movement.direction, movement.length)

    head_position = new_position
end
